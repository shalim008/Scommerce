using System;
using System.Collections.Generic;
namespace Core.Entities
{
    public class ProductVariationsAttributeMap : BaseEntity
    {
         public int ProductVariationsId { get; set; }
         public ProductVariations ProductVariations { get; set; }
         public int ProductAttributeId { get; set; }
         public int ProductAttributeValuesId { get; set; }
    }
}