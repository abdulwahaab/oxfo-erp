using AutoMapper;
using OxfoHome.DAL.DTOs;
using OxfoHome.Data.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace OxfoHome.DAL.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public PaymentService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _uow = unitOfWork;
        }

        public async Task<IQueryable<PaymentDTO>> GetAll()
        {
            IEnumerable<PaymentDTO> dtoList = _mapper.Map<IEnumerable<PaymentDTO>>(await _uow.Payments.GetAllAsync());
            return dtoList.AsQueryable();
        }

        public async Task<PagedResult<PaymentDTO>> GetPagedListAsync(int pageIndex, int pageSize,
        Func<IQueryable<PaymentDTO>, IOrderedQueryable<PaymentDTO>>? orderBy = null,
        Expression<Func<PaymentDTO, bool>>? filter = null)
        {
            var query = _uow.Payments.Table
            .Join(_uow.PayingEntities.Table,
            payment => payment.ToPayeeId,
            payee => payee.Id,
            (payment, toPayee) => new { payment, ToPayeeName = toPayee.Name })
            .Join(_uow.PayingEntities.Table,
            temp => temp.payment.FromPayeeId,
            payee => payee.Id,
            (temp, fromPayee) => new PaymentDTO
            {
                Id = temp.payment.Id,
                ToPayeeId = temp.payment.ToPayeeId,
                FromPayeeId = temp.payment.FromPayeeId,
                ToPayeeName = temp.ToPayeeName,
                FromPayeeName = fromPayee.Name,
                Amount = temp.payment.Amount,
                CreatedOn = temp.payment.CreatedOn,
                Status = temp.payment.Status
            });

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            var totalItems = query.Count();

            List<PaymentDTO> itemList;

            if (pageIndex > 0 && pageSize > 0)
                itemList = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            else
                itemList = await query.ToListAsync();

            return new PagedResult<PaymentDTO>
            {
                Items = itemList,
                TotalCount = totalItems
            };
        }

        public async Task<PaymentDTO?> GetByID(object id)
        {
            return await _uow.Payments.Table
            .Join(_uow.PayingEntities.Table,
            payment => payment.ToPayeeId,
            payee => payee.Id,
            (payment, toPayee) => new { payment, ToPayeeName = toPayee.Name })
            .Join(_uow.PayingEntities.Table,
            temp => temp.payment.FromPayeeId,
            payee => payee.Id,
            (temp, fromPayee) => new PaymentDTO
            {
                Id = temp.payment.Id,
                FromPayeeId = temp.payment.FromPayeeId,
                ToPayeeId = temp.payment.ToPayeeId,
                ToPayeeName = temp.ToPayeeName,
                FromPayeeName = fromPayee.Name,
                Amount = temp.payment.Amount,
                TransactionType = temp.payment.TransactionType,
                Attachment = temp.payment.Attachment,
                Notes = temp.payment.Notes,
                CreatedOn = temp.payment.CreatedOn,
                Status = temp.payment.Status
            }).Where(x => x.Id == (int)id).FirstOrDefaultAsync();
        }

        public async Task<bool> Add(PaymentDTO dto)
        {
            await _uow.Payments.AddAsync(_mapper.Map<Payment>(dto));
            return await _uow.SaveAsync() > 0;
        }

        public async Task<bool> Update(PaymentDTO dto)
        {
            Payment entity = await _uow.Payments.GetByIdAsync(dto.Id);

            entity.Amount = dto.Amount;
            entity.Notes = dto.Notes;
            entity.TransactionType = dto.TransactionType;
            entity.ToPayeeId = dto.ToPayeeId;
            entity.FromPayeeId = dto.FromPayeeId;
            entity.Attachment = dto.Attachment;

            await _uow.Payments.UpdateAsync(_mapper.Map<Payment>(entity));
            return await _uow.SaveAsync() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            await _uow.Payments.DeleteAsync(id);
            return await _uow.SaveAsync() > 0;
        }

        //get payment stats
        public async Task<AccountBalance?> GetAccountData(int id)
        {
            AccountBalance accountData = new AccountBalance();

            accountData.TotalPaid = _uow.Payments.Table.Where(x => x.FromPayeeId == id).Sum(x => x.Amount);
            accountData.TotalReceived = _uow.Payments.Table.Where(x => x.ToPayeeId == id).Sum(x => x.Amount);

            return accountData;
        }
    }
}
