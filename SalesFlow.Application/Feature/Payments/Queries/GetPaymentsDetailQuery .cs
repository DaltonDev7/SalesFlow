

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Application.Wrappers;

namespace SalesFlow.Application.Feature.Payments.Queries
{
    public class GetPaymentsDetailQuery : IRequest<ApiResponse<List<GetPaymentsDetail>>>
    {

    }

    public class GetPaymentsDetailQueryHandler : IRequestHandler<GetPaymentsDetailQuery, ApiResponse<List<GetPaymentsDetail>>>
    {

        private readonly IPaymentsRepository _paymentsRepository;

        public GetPaymentsDetailQueryHandler(IPaymentsRepository paymentsRepository)
        {
            _paymentsRepository = paymentsRepository;
        }

        public async Task<ApiResponse<List<GetPaymentsDetail>>> Handle(GetPaymentsDetailQuery request, CancellationToken cancellationToken)
        {
            var data = await _paymentsRepository.GetPaymentsDetail();

            return new ApiResponse<List<GetPaymentsDetail>>(data);
        }
    }

}
