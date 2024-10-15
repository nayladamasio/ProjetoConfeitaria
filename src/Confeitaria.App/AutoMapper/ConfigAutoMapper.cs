using AutoMapper;
using Confeitaria.App.ViewModels;
using Confeitaria.Business.Models;

namespace Confeitaria.App.AutoMapper
{
    public class ConfigAutoMapper : Profile
    {
        public ConfigAutoMapper()
        {
            CreateMap<Cliente, ClienteViewModel>().ReverseMap();
            CreateMap<Pedido, PedidoViewModel>().ReverseMap();
            CreateMap<EnderecoPedido, EnderecoPedidoViewModel>().ReverseMap();
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
            CreateMap<FaleConosco, FaleConoscoViewModel>().ReverseMap();
        }
    }
}
