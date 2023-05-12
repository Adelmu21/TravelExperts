using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelExpertsData
{
    [Table("Packages_Products_Suppliers")]
    [Index(nameof(PackageId), Name = "PackagesPackages_Products_Suppliers")]
    [Index(nameof(ProductSupplierId), Name = "ProductSupplierId")]
    [Index(nameof(ProductSupplierId), Name = "Products_SuppliersPackages_Products_Suppliers")]
    [Index(nameof(PackageId), nameof(ProductSupplierId), Name = "UQ__Packages__29CA8E95BFFB8774", IsUnique = true)]
    public partial class PackagesProductsSupplier
    {
        [Key]
        public int PackageProductSupplierId { get; set; }
        public int PackageId { get; set; }
        public int ProductSupplierId { get; set; }

        [ForeignKey(nameof(PackageId))]
        [InverseProperty("PackagesProductsSuppliers")]
        public virtual Package Package { get; set; } = null!;
        [ForeignKey(nameof(ProductSupplierId))]
        [InverseProperty(nameof(ProductsSupplier.PackagesProductsSuppliers))]
        public virtual ProductsSupplier ProductSupplier { get; set; } = null!;
    }
}
