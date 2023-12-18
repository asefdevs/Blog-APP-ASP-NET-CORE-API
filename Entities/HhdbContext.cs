using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace newProject.Entities;

public partial class HhdbContext : DbContext
{
    public HhdbContext()
    {
    }

    public HhdbContext(DbContextOptions<HhdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<AddressService> AddressServices { get; set; }

    public virtual DbSet<AddressServiceType> AddressServiceTypes { get; set; }

    public virtual DbSet<AuthorizationType> AuthorizationTypes { get; set; }

    public virtual DbSet<Building> Buildings { get; set; }

    public virtual DbSet<Citizen> Citizens { get; set; }

    public virtual DbSet<CitizenAddress> CitizenAddresses { get; set; }

    public virtual DbSet<CitizenService> CitizenServices { get; set; }

    public virtual DbSet<CitizenVeriTest> CitizenVeriTests { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<ContactPerson> ContactPeople { get; set; }

    public virtual DbSet<DeliveryStatus> DeliveryStatuses { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Door> Doors { get; set; }

    public virtual DbSet<FailReasonType> FailReasonTypes { get; set; }

    public virtual DbSet<FeedbackSubTopic> FeedbackSubTopics { get; set; }

    public virtual DbSet<FeedbackTopic> FeedbackTopics { get; set; }

    public virtual DbSet<Neighbourhood> Neighbourhoods { get; set; }

    public virtual DbSet<Parameter> Parameters { get; set; }

    public virtual DbSet<SecurityIndexType> SecurityIndexTypes { get; set; }

    public virtual DbSet<ServiceType> ServiceTypes { get; set; }

    public virtual DbSet<Street> Streets { get; set; }

    public virtual DbSet<SupportPackage> SupportPackages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAddress> UserAddresses { get; set; }

    public virtual DbSet<UserOtpMessage> UserOtpMessages { get; set; }

    public virtual DbSet<UserRequestCount> UserRequestCounts { get; set; }

    public virtual DbSet<Visit> Visits { get; set; }

    public virtual DbSet<VisitInteraction> VisitInteractions { get; set; }

    public virtual DbSet<VisitInteractionDelivery> VisitInteractionDeliveries { get; set; }

    public virtual DbSet<VisitInteractionFailReason> VisitInteractionFailReasons { get; set; }

    public virtual DbSet<VisitInteractionFeedback> VisitInteractionFeedbacks { get; set; }

    public virtual DbSet<VisitInteractionSecurityIndex> VisitInteractionSecurityIndices { get; set; }

    public virtual DbSet<VisitInteractionSupportPackage> VisitInteractionSupportPackages { get; set; }

    public virtual DbSet<VisitInteractionType> VisitInteractionTypes { get; set; }

    public virtual DbSet<VisitTeam> VisitTeams { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:conn");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Turkish_CI_AS");

        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("Address");

            entity.Property(e => e.BuildingId).HasColumnName("Building_Id");
            entity.Property(e => e.CityId).HasColumnName("City_Id");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.DistrictId).HasColumnName("District_Id");
            entity.Property(e => e.DoorId).HasColumnName("Door_Id");
            entity.Property(e => e.NeighbourhoodId).HasColumnName("Neighbourhood_Id");
            entity.Property(e => e.StreetId).HasColumnName("Street_Id");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Building).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.BuildingId)
                .HasConstraintName("fk_Address_Building_id");

            entity.HasOne(d => d.City).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("fk_Address_City_Id");

            entity.HasOne(d => d.District).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.DistrictId)
                .HasConstraintName("fk_Address_District_Id");

            entity.HasOne(d => d.Door).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.DoorId)
                .HasConstraintName("fk_Address_Door_id");

            entity.HasOne(d => d.Neighbourhood).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.NeighbourhoodId)
                .HasConstraintName("fk_Address_Neighbourhood_Id");

            entity.HasOne(d => d.Street).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.StreetId)
                .HasConstraintName("fk_Address_Street_Id");
        });

        modelBuilder.Entity<AddressService>(entity =>
        {
            entity.ToTable("AddressService");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.DistrictId).HasColumnName("District_Id");
            entity.Property(e => e.NeighbourhoodId).HasColumnName("Neighbourhood_Id");
            entity.Property(e => e.Responsible).HasMaxLength(250);
            entity.Property(e => e.ServiceName).HasMaxLength(500);
            entity.Property(e => e.ServiceSubType).HasMaxLength(500);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.District).WithMany(p => p.AddressServices)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AddressService_District");

            entity.HasOne(d => d.Neighbourhood).WithMany(p => p.AddressServices)
                .HasForeignKey(d => d.NeighbourhoodId)
                .HasConstraintName("FK_AddressService_Neighbourhood");
        });

        modelBuilder.Entity<AddressServiceType>(entity =>
        {
            entity.ToTable("AddressServiceType");

            entity.Property(e => e.AddressId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("Address_Id");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.ServiceTypeId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("ServiceType_Id");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Address).WithMany(p => p.AddressServiceTypes)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("fk_AddressServiceType_Address_Id");

            entity.HasOne(d => d.ServiceType).WithMany(p => p.AddressServiceTypes)
                .HasForeignKey(d => d.ServiceTypeId)
                .HasConstraintName("fk_AddressServiceType_ServiceType_Id");
        });

        modelBuilder.Entity<AuthorizationType>(entity =>
        {
            entity.ToTable("AuthorizationType");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.IsPassive).HasColumnName("is_passive");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Building>(entity =>
        {
            entity.ToTable("Building");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.StreetId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("Street_Id");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Value)
                .HasMaxLength(50)
                .UseCollation("Turkish_100_CI_AS");

            entity.HasOne(d => d.Street).WithMany(p => p.Buildings)
                .HasForeignKey(d => d.StreetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Building_Street_id");
        });

        modelBuilder.Entity<Citizen>(entity =>
        {
            entity.ToTable("Citizen");

            entity.Property(e => e.BirthDate).HasColumnType("datetime");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.FirstName)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.LastName)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.Tckn)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<CitizenAddress>(entity =>
        {
            entity.ToTable("CitizenAddress");

            entity.Property(e => e.AddressId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("Address_Id");
            entity.Property(e => e.CitizenId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("Citizen_Id");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Address).WithMany(p => p.CitizenAddresses)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("fk_CitizenAddress_Address_Id");

            entity.HasOne(d => d.Citizen).WithMany(p => p.CitizenAddresses)
                .HasForeignKey(d => d.CitizenId)
                .HasConstraintName("fk_CitizenAddress_Citizen_Id");
        });

        modelBuilder.Entity<CitizenService>(entity =>
        {
            entity.ToTable("CitizenService");

            entity.Property(e => e.CitizenId).HasColumnName("Citizen_Id");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.ServiceName)
                .HasMaxLength(500)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.ServiceTypeId).HasColumnName("ServiceType_Id");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Citizen).WithMany(p => p.CitizenServices)
                .HasForeignKey(d => d.CitizenId)
                .HasConstraintName("FK_CitizenService_Citizen_Id");

            entity.HasOne(d => d.ServiceType).WithMany(p => p.CitizenServices)
                .HasForeignKey(d => d.ServiceTypeId)
                .HasConstraintName("FK_CitizenService_ServiceType_Id");
        });

        modelBuilder.Entity<CitizenVeriTest>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("citizen_veri_test");

            entity.Property(e => e.Adi)
                .HasMaxLength(50)
                .HasColumnName("adi");
            entity.Property(e => e.Adrescaddesokak)
                .HasMaxLength(50)
                .HasColumnName("adrescaddesokak");
            entity.Property(e => e.Adresiladi)
                .HasMaxLength(50)
                .HasColumnName("adresiladi");
            entity.Property(e => e.Adresilceadi)
                .HasMaxLength(50)
                .HasColumnName("adresilceadi");
            entity.Property(e => e.Adresmuhtarlikadi)
                .HasMaxLength(50)
                .HasColumnName("adresmuhtarlikadi");
            entity.Property(e => e.Cinsiyeti)
                .HasMaxLength(50)
                .HasColumnName("cinsiyeti");
            entity.Property(e => e.Daireno)
                .HasMaxLength(50)
                .HasColumnName("daireno");
            entity.Property(e => e.Dogumtarihi)
                .HasMaxLength(50)
                .HasColumnName("dogumtarihi");
            entity.Property(e => e.Kapino)
                .HasMaxLength(50)
                .HasColumnName("kapino");
            entity.Property(e => e.Nufusilcesi)
                .HasMaxLength(50)
                .HasColumnName("nufusilcesi");
            entity.Property(e => e.Nufusili)
                .HasMaxLength(50)
                .HasColumnName("nufusili");
            entity.Property(e => e.PrimaryMobilePhone)
                .HasMaxLength(50)
                .HasColumnName("primary_mobile_phone");
            entity.Property(e => e.Soyadi)
                .HasMaxLength(50)
                .HasColumnName("soyadi");
            entity.Property(e => e.Tckn)
                .HasMaxLength(50)
                .HasColumnName("tckn");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.ToTable("City");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Value)
                .HasMaxLength(50)
                .UseCollation("Turkish_100_CI_AS");
        });

        modelBuilder.Entity<ContactPerson>(entity =>
        {
            entity.ToTable("ContactPerson");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.Note).UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.Tckn)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<DeliveryStatus>(entity =>
        {
            entity.ToTable("DeliveryStatus");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Value)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.ToTable("District");

            entity.Property(e => e.CityId).HasColumnName("City_Id");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Value)
                .HasMaxLength(50)
                .UseCollation("Turkish_100_CI_AS");

            entity.HasOne(d => d.City).WithMany(p => p.Districts)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_district_city_id");
        });

        modelBuilder.Entity<Door>(entity =>
        {
            entity.ToTable("Door");

            entity.Property(e => e.BuildingId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("Building_Id");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Value)
                .HasMaxLength(50)
                .UseCollation("Turkish_100_CI_AS");

            entity.HasOne(d => d.Building).WithMany(p => p.Doors)
                .HasForeignKey(d => d.BuildingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Door_Building_id");
        });

        modelBuilder.Entity<FailReasonType>(entity =>
        {
            entity.ToTable("FailReasonType");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Value)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
        });

        modelBuilder.Entity<FeedbackSubTopic>(entity =>
        {
            entity.ToTable("FeedbackSubTopic");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.FeedbackTopicId).HasColumnName("FeedbackTopic_Id");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Value)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");

            entity.HasOne(d => d.FeedbackTopic).WithMany(p => p.FeedbackSubTopics)
                .HasForeignKey(d => d.FeedbackTopicId)
                .HasConstraintName("FK_FeedbackSubTopic_FeedbackTopic");
        });

        modelBuilder.Entity<FeedbackTopic>(entity =>
        {
            entity.ToTable("FeedbackTopic");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Value)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
        });

        modelBuilder.Entity<Neighbourhood>(entity =>
        {
            entity.ToTable("Neighbourhood");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.DistrictId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("District_Id");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Value)
                .HasMaxLength(50)
                .UseCollation("Turkish_100_CI_AS");

            entity.HasOne(d => d.District).WithMany(p => p.Neighbourhoods)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Neighbourhood_district_id");
        });

        modelBuilder.Entity<Parameter>(entity =>
        {
            entity.ToTable("Parameter");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.ExtraValue1)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.ExtraValue2)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.ExtraValue3)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.PKey)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS")
                .HasColumnName("P_Key");
            entity.Property(e => e.PType)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS")
                .HasColumnName("P_Type");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<SecurityIndexType>(entity =>
        {
            entity.ToTable("SecurityIndexType");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Value)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
        });

        modelBuilder.Entity<ServiceType>(entity =>
        {
            entity.ToTable("ServiceType");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Value)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
        });

        modelBuilder.Entity<Street>(entity =>
        {
            entity.ToTable("Street");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.NeighbourhoodId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("Neighbourhood_Id");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Value)
                .HasMaxLength(50)
                .UseCollation("Turkish_100_CI_AS");

            entity.HasOne(d => d.Neighbourhood).WithMany(p => p.Streets)
                .HasForeignKey(d => d.NeighbourhoodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Street_Neighbourhood_id");
        });

        modelBuilder.Entity<SupportPackage>(entity =>
        {
            entity.ToTable("SupportPackage");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Value)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.BirthDate).HasColumnType("date");
            entity.Property(e => e.CountDate).HasColumnType("datetime");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.FullName)
                .HasMaxLength(300)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.Password).UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.Surname)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.Tckn)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.AuthorizationType).WithMany(p => p.Users)
                .HasForeignKey(d => d.AuthorizationTypeId)
                .HasConstraintName("FK_User_AuthorizationType_Id");
        });

        modelBuilder.Entity<UserAddress>(entity =>
        {
            entity.ToTable("UserAddress");

            entity.Property(e => e.AddressId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("Address_Id");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.UserId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("User_Id");

            entity.HasOne(d => d.Address).WithMany(p => p.UserAddresses)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("fk_UserAddress_Address_Id");

            entity.HasOne(d => d.User).WithMany(p => p.UserAddresses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_UserAddress_User_Id");
        });

        modelBuilder.Entity<UserOtpMessage>(entity =>
        {
            entity.ToTable("UserOtpMessage");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.MessageId)
                .HasMaxLength(300)
                .HasColumnName("Message_Id");
            entity.Property(e => e.MessageTime).HasColumnType("datetime");
            entity.Property(e => e.OtpCode).HasMaxLength(300);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.User).WithMany(p => p.UserOtpMessages)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_UserOtpMessage_User_Id");
        });

        modelBuilder.Entity<UserRequestCount>(entity =>
        {
            entity.ToTable("UserRequestCount");

            entity.Property(e => e.CountDate).HasColumnType("datetime");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Method).HasMaxLength(300);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.UserId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("User_Id");

            entity.HasOne(d => d.User).WithMany(p => p.UserRequestCounts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_UserRequestCount_User_Id");
        });

        modelBuilder.Entity<Visit>(entity =>
        {
            entity.ToTable("Visit");

            entity.Property(e => e.AddressId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("Address_Id");
            entity.Property(e => e.AuthorizationTypeId).HasColumnName("AuthorizationType_Id");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.DifferentPersonId).HasColumnName("DifferentPerson_Id");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.UserId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("User_Id");

            entity.HasOne(d => d.Address).WithMany(p => p.Visits)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("fk_Visit_Address_Id");

            entity.HasOne(d => d.AuthorizationType).WithMany(p => p.Visits)
                .HasForeignKey(d => d.AuthorizationTypeId)
                .HasConstraintName("FK_Visit_AuthorizationType");

            entity.HasOne(d => d.DifferentPerson).WithMany(p => p.Visits)
                .HasForeignKey(d => d.DifferentPersonId)
                .HasConstraintName("FK_Visit_ContactPerson");

            entity.HasOne(d => d.User).WithMany(p => p.Visits)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_Visit_User_Id");
        });

        modelBuilder.Entity<VisitInteraction>(entity =>
        {
            entity.ToTable("VisitInteraction");

            entity.Property(e => e.ContactPersonId).HasColumnName("ContactPerson_Id");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.VisitId).HasColumnName("Visit_Id");
            entity.Property(e => e.VisitInteractionTypeId).HasColumnName("VisitInteractionType_Id");

            entity.HasOne(d => d.ContactPerson).WithMany(p => p.VisitInteractions)
                .HasForeignKey(d => d.ContactPersonId)
                .HasConstraintName("FK_VisitInteraction_ContactPerson");

            entity.HasOne(d => d.Visit).WithMany(p => p.VisitInteractions)
                .HasForeignKey(d => d.VisitId)
                .HasConstraintName("FK_User_Visit_Id");

            entity.HasOne(d => d.VisitInteractionType).WithMany(p => p.VisitInteractions)
                .HasForeignKey(d => d.VisitInteractionTypeId)
                .HasConstraintName("FK_User_VisitInteractionType_Id");
        });

        modelBuilder.Entity<VisitInteractionDelivery>(entity =>
        {
            entity.ToTable("VisitInteractionDelivery");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.DeliveryDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.VisitInteractionId).HasColumnName("VisitInteraction_Id");
            entity.Property(e => e.VisitInteractionSupportPackageId).HasColumnName("VisitInteractionSupportPackage_Id");

            entity.HasOne(d => d.VisitInteraction).WithMany(p => p.VisitInteractionDeliveries)
                .HasForeignKey(d => d.VisitInteractionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VisitInteractionSupportPackage_VisitInteraction_Id");

            entity.HasOne(d => d.VisitInteractionSupportPackage).WithMany(p => p.VisitInteractionDeliveries)
                .HasForeignKey(d => d.VisitInteractionSupportPackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VisitInteractionDelivery_VisitInteractionSupportPackage_Id");
        });

        modelBuilder.Entity<VisitInteractionFailReason>(entity =>
        {
            entity.ToTable("VisitInteractionFailReason");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.FailReasonTypeId).HasColumnName("FailReasonType_Id");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.VisitInteractionId).HasColumnName("VisitInteraction_Id");

            entity.HasOne(d => d.FailReasonType).WithMany(p => p.VisitInteractionFailReasons)
                .HasForeignKey(d => d.FailReasonTypeId)
                .HasConstraintName("FK_VisitInteractionFailReason_FailReasonType");

            entity.HasOne(d => d.VisitInteraction).WithMany(p => p.VisitInteractionFailReasons)
                .HasForeignKey(d => d.VisitInteractionId)
                .HasConstraintName("FK_VisitInteractionFailReason_VisitInteraction");
        });

        modelBuilder.Entity<VisitInteractionFeedback>(entity =>
        {
            entity.ToTable("VisitInteractionFeedback");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.FeedbackSubTopicId).HasColumnName("FeedbackSubTopic_Id");
            entity.Property(e => e.FeedbackTopicId).HasColumnName("FeedbackTopic_Id");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.VisitInteractionId).HasColumnName("VisitInteraction_Id");

            entity.HasOne(d => d.FeedbackSubTopic).WithMany(p => p.VisitInteractionFeedbacks)
                .HasForeignKey(d => d.FeedbackSubTopicId)
                .HasConstraintName("FK_VisitInteractionFeedback_FeedbackSubTopic");

            entity.HasOne(d => d.FeedbackTopic).WithMany(p => p.VisitInteractionFeedbacks)
                .HasForeignKey(d => d.FeedbackTopicId)
                .HasConstraintName("FK_VisitInteractionFeedback_FeedbackTopic");

            entity.HasOne(d => d.VisitInteraction).WithMany(p => p.VisitInteractionFeedbacks)
                .HasForeignKey(d => d.VisitInteractionId)
                .HasConstraintName("FK_VisitInteractionFeedback_VisitInteraction");
        });

        modelBuilder.Entity<VisitInteractionSecurityIndex>(entity =>
        {
            entity.ToTable("VisitInteractionSecurityIndex");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.SecurityIndexTypeId).HasColumnName("SecurityIndexType_Id");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.VisitInteractionId).HasColumnName("VisitInteraction_Id");

            entity.HasOne(d => d.SecurityIndexType).WithMany(p => p.VisitInteractionSecurityIndices)
                .HasForeignKey(d => d.SecurityIndexTypeId)
                .HasConstraintName("FK_VisitInteractionSecurityIndex_SecurityIndexType");

            entity.HasOne(d => d.VisitInteraction).WithMany(p => p.VisitInteractionSecurityIndices)
                .HasForeignKey(d => d.VisitInteractionId)
                .HasConstraintName("FK_VisitInteractionSecurityIndex_VisitInteraction");
        });

        modelBuilder.Entity<VisitInteractionSupportPackage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_VisitSupportPackage");

            entity.ToTable("VisitInteractionSupportPackage");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Pending)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.SupportPackageId).HasColumnName("SupportPackage_Id");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.VisitInteractionDeliveryId).HasColumnName("VisitInteractionDelivery_Id");
            entity.Property(e => e.VisitInteractionId).HasColumnName("VisitInteraction_Id");

            entity.HasOne(d => d.SupportPackage).WithMany(p => p.VisitInteractionSupportPackages)
                .HasForeignKey(d => d.SupportPackageId)
                .HasConstraintName("FK_VisitSupportPackage_SupportPackage_Id");

            entity.HasOne(d => d.VisitInteractionDelivery).WithMany(p => p.VisitInteractionSupportPackages)
                .HasForeignKey(d => d.VisitInteractionDeliveryId)
                .HasConstraintName("FK_VisitInteractionSupportPackage_VisitInteractionDelivery");

            entity.HasOne(d => d.VisitInteraction).WithMany(p => p.VisitInteractionSupportPackages)
                .HasForeignKey(d => d.VisitInteractionId)
                .HasConstraintName("FK_VisitSupportPackage_VisitInteraction_Id");
        });

        modelBuilder.Entity<VisitInteractionType>(entity =>
        {
            entity.ToTable("VisitInteractionType");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Value)
                .HasMaxLength(200)
                .UseCollation("Turkish_100_CI_AS");
        });

        modelBuilder.Entity<VisitTeam>(entity =>
        {
            entity.ToTable("VisitTeam");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.IsFinished).HasColumnName("Is_Finished");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("User_Id");
            entity.Property(e => e.VisitId).HasColumnName("Visit_Id");

            entity.HasOne(d => d.User).WithMany(p => p.VisitTeams)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_VisitTeam_User_Id");

            entity.HasOne(d => d.Visit).WithMany(p => p.VisitTeams)
                .HasForeignKey(d => d.VisitId)
                .HasConstraintName("fk_VisitTeam_Visit_Id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
