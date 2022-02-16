using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Apps.AdminApi.DTOs;
using Shop.Api.Apps.AdminApi.DTOs.ProductDtos;
using Shop.Api.Extensions;
using Shop.Core.Entities;
using Shop.Core.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Api.Controllers
{
    [Route("admin/api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public ProductsController(IProductRepository productRepository,IMapper mapper, IWebHostEnvironment env)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _env=env;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Product product =await _productRepository.GetAsync(x => x.Id == id && !x.IsDeleted, "Products");

            if (product == null) return NotFound();



            ProductGetDto productDto = _mapper.Map<ProductGetDto>(product);



            return Ok(productDto);
        }

        [Route("")]
        [HttpGet]
        public IActionResult GetAll(int page = 1, string search = null)
        {
            var query = _productRepository.GetAll(x => !x.IsDeleted,"Category");

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(x => x.Name.Contains(search));

            ListDto<ProductListItemDto> listDto = new ListDto<ProductListItemDto>
            {
                Items = query.Skip((page - 1) * 8).Take(8).Select(x =>
                    new ProductListItemDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Image=x.Image,
                        SalePrice =x.SalePrice,
                        CostPrice =x.CostPrice,
                        Category = new CategoryInProductListItemDto
                        {

                            Id = x.CategoryId,
                            Name = x.Category.Name


                        }

                    }).ToList(),
                TotalCount = query.Count()
            };




            return Ok(listDto);
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Create(ProductPostDto productDto)
        {
            //Product product = new Product
            //{
            //    Name = productDto.Name,
            //    SalePrice = productDto.SalePrice,
            //    CostPrice = productDto.CostPrice,
            //    CategoryId = productDto.CategoryId

            //};
            Product product = _mapper.Map<Product>(productDto);
            product.Image = productDto.Image.SaveImg(_env.WebRootPath, "ProductImg");
            await _productRepository.AddAsync(product);
            await _productRepository.CommitAsync(); 

            return StatusCode(201, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductPostDto productDto)
        {
            Product existProduct = await _productRepository.GetAsync(x => x.Id == id);

            if (existProduct == null)
                return NotFound();

            if (productDto.Image != null)
            {


                Helpers.Helper.DeleteImg(_env.WebRootPath, "ProductImg", existProduct.Image);
                existProduct.Image = productDto.Image.SaveImg(_env.WebRootPath, "ProductImg");
            }
            if (existProduct.CategoryId != productDto.CategoryId && !await _productRepository.IsExistAsync(c => c.Id == productDto.CategoryId && !c.IsDeleted))
                return NotFound();
            existProduct.CategoryId = productDto.CategoryId;
            existProduct.Name = productDto.Name;
            existProduct.SalePrice=productDto.SalePrice;
           

            await _productRepository.CommitAsync();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Product product =await  _productRepository.GetAsync(x => x.Id == id);

            if (product == null)
                return NotFound();
            Helpers.Helper.DeleteImg(_env.WebRootPath, "ProductImg", product.Image);
            product.IsDeleted= true;
            await _productRepository.CommitAsync();


            return NoContent();
        }

    }
}
