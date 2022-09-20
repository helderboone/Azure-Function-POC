using AzureFunctionPOC.Domain.ShoppingCart;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace AzureFunctionPOC.Infra.ShoppingCartInfra.Mappings;

public class ShoppingCartMapping : IEntityTypeConfiguration<ShoppingCart>
{
    public void Configure(EntityTypeBuilder<ShoppingCart> builder)
    {
        builder.HasIndex(x => x.CustomerId).HasName("IDX_Customer");

        builder.Ignore(x => x.Voucher)
            .OwnsOne(x => x.Voucher, v =>
            {
                v.Property(vc => vc.Code).HasColumnType("varchar(50)");

                v.Property(vc => vc.DiscountType);

                v.Property(vc => vc.Percentage);

                v.Property(vc => vc.Discount);
            });

        //EF Relationship
        builder.HasMany(sc => sc.Items)
                .WithOne(i => i.ShoppingCart)
                .HasForeignKey(ci => ci.ShoppingCartId);
    }
}
