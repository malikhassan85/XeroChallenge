using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XeroChallenge.Domain.Entities;
using XeroChallenge.WebApi.Models;

namespace XeroChallenge.WebApi.Extensions
{
    public static class ModelConvertor
    {
        public static ProductDto[] ToDto(this IList<Product> models)
        {
            if (models == null)
            {
                return null;
            }

            return models.Select(model => new ProductDto()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                DeliveryPrice = model.DeliveryPrice
            }).ToArray();
        }

        public static ProductDto ToDto(this Product model)
        {
            if (model == null)
            {
                return null;
            }

            return new ProductDto()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                DeliveryPrice = model.DeliveryPrice
            };
        }

        public static Product ToDomainEntity(this ProductDto model)
        {
            if (model == null)
            {
                return null;
            }

            return new Product()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Price = model.Price.GetValueOrDefault(),
                DeliveryPrice = model.DeliveryPrice.GetValueOrDefault()
            };
        }

        public static ProductOptionDto[] ToDto(this IList<ProductOption> models)
        {
            if (models == null)
            {
                return null;
            }

            return models.Select(model => new ProductOptionDto()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description
            }).ToArray();
        }

        public static ProductOptionDto ToDto(this ProductOption model)
        {
            if (model == null)
            {
                return null;
            }

            return new ProductOptionDto()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description
            };
        }

        public static ProductOption ToDomainEntity(this ProductOptionDto model)
        {
            if (model == null)
            {
                return null;
            }

            return new ProductOption()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description
            };
        }
    }
}
