﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrivateHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionConverters;
using PrivateHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;
using PrivateHistoryService.Infrastructure.EntityFramework.Models;

namespace PrivateHistoryService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration
{
    internal sealed class TopicSubscriptionConfiguration : IEntityTypeConfiguration<TopicSubscriptionReadModel>
    {
        private readonly IEncryptionProvider _encryptionProvider;

        public TopicSubscriptionConfiguration(IEncryptionProvider encryptionProvider)
        {
            _encryptionProvider = encryptionProvider;
        }

        public void Configure(EntityTypeBuilder<TopicSubscriptionReadModel> builder)
        {
            //Table name
            builder.ToTable("TopicSubscription");

            //Composite primary key
            builder.HasKey(key => new { key.TopicId, key.UserId });

            //Property config - Start

            builder.Property(p => p.TopicId)
                .IsRequired();

            builder.Property(p => p.UserId)
                .IsRequired();

            builder.Property(p => p.DateTime)
                .HasConversion(new EncryptedDateTimeOffsetConverter(_encryptionProvider))
                .IsRequired();

            //Property config - End

            //Relationships
            builder.HasOne(p => p.User)
                .WithMany(p => p.TopicSubscriptions)
                .HasForeignKey(p => p.UserId)
                .HasConstraintName("FK_User_TopicSubscriptions")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
