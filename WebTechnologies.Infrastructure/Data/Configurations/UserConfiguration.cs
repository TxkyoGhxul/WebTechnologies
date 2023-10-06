using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebTechnologies.Domain.Models;
using WebTechnologies.Domain.ValueObjects;

namespace WebTechnologies.Infrastructure.Data.Configurations;
internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Name).IsRequired().HasMaxLength(25);

        builder.Property(b => b.Email).HasConversion(
            email => email.Value,
            value => Email.From(value));

        builder.Property(b => b.BirthDate).HasConversion(
            dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue),
            dateTime => DateOnly.FromDateTime(dateTime));
    }
}
