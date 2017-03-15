using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using IOPTCore.Models;

namespace IOPTCore.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20170313122816_Migration4")]
    partial class Migration4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("IOPTCore.Models.AppKey", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("appKey");

                    b.Property<long?>("modelid");

                    b.HasKey("id");

                    b.HasIndex("modelid");

                    b.ToTable("AppKeys");
                });

            modelBuilder.Entity("IOPTCore.Models.Dashboard", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("objectId");

                    b.HasKey("id");

                    b.ToTable("Dashboards");
                });

            modelBuilder.Entity("IOPTCore.Models.Dashboard+PropertyMap", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("dashboardId");

                    b.Property<bool>("isControl");

                    b.Property<double?>("max");

                    b.Property<double?>("min");

                    b.Property<long?>("propertyid");

                    b.HasKey("id");

                    b.HasIndex("dashboardId");

                    b.HasIndex("propertyid");

                    b.ToTable("PropertyMap");
                });

            modelBuilder.Entity("IOPTCore.Models.Model", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("Userid");

                    b.Property<string>("name");

                    b.Property<string>("pathUnit");

                    b.HasKey("id");

                    b.HasIndex("Userid");

                    b.ToTable("Models");
                });

            modelBuilder.Entity("IOPTCore.Models.Object", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("modelId");

                    b.Property<string>("name");

                    b.Property<string>("pathUnit");

                    b.HasKey("id");

                    b.HasIndex("modelId");

                    b.ToTable("Objects");
                });

            modelBuilder.Entity("IOPTCore.Models.Property", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("name");

                    b.Property<long>("objectId");

                    b.Property<string>("pathUnit");

                    b.Property<int>("type");

                    b.Property<string>("value");

                    b.HasKey("id");

                    b.HasIndex("objectId");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("IOPTCore.Models.Script", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("name");

                    b.Property<string>("pathUnit");

                    b.Property<long>("propertyId");

                    b.Property<string>("value");

                    b.HasKey("id");

                    b.HasIndex("propertyId");

                    b.ToTable("Scripts");
                });

            modelBuilder.Entity("IOPTCore.Models.User", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("login")
                        .IsRequired();

                    b.Property<string>("password")
                        .IsRequired();

                    b.HasKey("id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("IOPTCore.Models.AppKey", b =>
                {
                    b.HasOne("IOPTCore.Models.Model", "model")
                        .WithMany()
                        .HasForeignKey("modelid");
                });

            modelBuilder.Entity("IOPTCore.Models.Dashboard+PropertyMap", b =>
                {
                    b.HasOne("IOPTCore.Models.Dashboard")
                        .WithMany("view")
                        .HasForeignKey("dashboardId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("IOPTCore.Models.Property", "property")
                        .WithMany()
                        .HasForeignKey("propertyid");
                });

            modelBuilder.Entity("IOPTCore.Models.Model", b =>
                {
                    b.HasOne("IOPTCore.Models.User")
                        .WithMany("Models")
                        .HasForeignKey("Userid");
                });

            modelBuilder.Entity("IOPTCore.Models.Object", b =>
                {
                    b.HasOne("IOPTCore.Models.Model")
                        .WithMany("objects")
                        .HasForeignKey("modelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("IOPTCore.Models.Property", b =>
                {
                    b.HasOne("IOPTCore.Models.Object")
                        .WithMany("properties")
                        .HasForeignKey("objectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("IOPTCore.Models.Script", b =>
                {
                    b.HasOne("IOPTCore.Models.Property")
                        .WithMany("scripts")
                        .HasForeignKey("propertyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
