﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SubscriptionService.Data;

#nullable disable

namespace SubscriptionService.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SubscriptionService.Models.GatewayRawEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("EventPayload")
                        .HasColumnType("jsonb");

                    b.Property<string>("EventType")
                        .HasColumnType("text");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("gateway_raw_event", (string)null);
                });

            modelBuilder.Entity("SubscriptionService.Models.PaymentGateway", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool?>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("Platform")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("payment_gateway", (string)null);
                });

            modelBuilder.Entity("SubscriptionService.Models.Subscription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<float?>("Amount")
                        .HasColumnType("real");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("Currency")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ExpiredAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("GatewaySubscriptionId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PaymentTransactionId")
                        .HasColumnType("uuid");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<Guid>("SubscriptionPlanId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PaymentTransactionId")
                        .IsUnique();

                    b.HasIndex("SubscriptionPlanId");

                    b.ToTable("subscription", (string)null);
                });

            modelBuilder.Entity("SubscriptionService.Models.SubscriptionPlan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool?>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("CostDisplay")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int?>("Duration")
                        .HasColumnType("integer");

                    b.Property<string>("FeaturedImage")
                        .HasColumnType("text");

                    b.Property<Guid>("Gateways")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Gateways");

                    b.ToTable("subscription_plan", (string)null);
                });

            modelBuilder.Entity("SubscriptionService.Models.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int?>("Amount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("Currency")
                        .HasColumnType("text");

                    b.Property<string>("ErrorMessage")
                        .HasColumnType("text");

                    b.Property<Guid?>("GatewayOrderId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("GatewayRawEventId")
                        .HasColumnType("uuid");

                    b.Property<string>("GatewayRefCode")
                        .HasColumnType("text");

                    b.Property<string>("GatewayState")
                        .HasColumnType("text");

                    b.Property<Guid?>("GatewayTransactionId")
                        .HasColumnType("uuid");

                    b.Property<string>("Platform")
                        .HasColumnType("text");

                    b.Property<bool?>("RenewableStatus")
                        .HasColumnType("boolean");

                    b.Property<string>("State")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("GatewayRawEventId")
                        .IsUnique();

                    b.ToTable("transaction", (string)null);
                });

            modelBuilder.Entity("SubscriptionService.Models.Subscription", b =>
                {
                    b.HasOne("SubscriptionService.Models.Transaction", "Transaction")
                        .WithOne("Subscription")
                        .HasForeignKey("SubscriptionService.Models.Subscription", "PaymentTransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SubscriptionService.Models.SubscriptionPlan", "SubscriptionPlan")
                        .WithMany("Subscriptions")
                        .HasForeignKey("SubscriptionPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SubscriptionPlan");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("SubscriptionService.Models.SubscriptionPlan", b =>
                {
                    b.HasOne("SubscriptionService.Models.PaymentGateway", "PaymentGateway")
                        .WithMany("SubscriptionPlans")
                        .HasForeignKey("Gateways")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaymentGateway");
                });

            modelBuilder.Entity("SubscriptionService.Models.Transaction", b =>
                {
                    b.HasOne("SubscriptionService.Models.GatewayRawEvent", "GatewayRawEvent")
                        .WithOne("Transaction")
                        .HasForeignKey("SubscriptionService.Models.Transaction", "GatewayRawEventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GatewayRawEvent");
                });

            modelBuilder.Entity("SubscriptionService.Models.GatewayRawEvent", b =>
                {
                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("SubscriptionService.Models.PaymentGateway", b =>
                {
                    b.Navigation("SubscriptionPlans");
                });

            modelBuilder.Entity("SubscriptionService.Models.SubscriptionPlan", b =>
                {
                    b.Navigation("Subscriptions");
                });

            modelBuilder.Entity("SubscriptionService.Models.Transaction", b =>
                {
                    b.Navigation("Subscription");
                });
#pragma warning restore 612, 618
        }
    }
}
