namespace Capstone.DAL.Context
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Capstone.DAL.Entities;

    public partial class CapstoneContext : DbContext
    {
        public CapstoneContext()
            : base("name=CapstoneDB")
        {
        }

        public virtual DbSet<TB_Capstone_ABSENCE_DETAIL> TB_Capstone_ABSENCE_DETAIL { get; set; }
        public virtual DbSet<TB_Capstone_ALLOCATION> TB_Capstone_ALLOCATION { get; set; }
        public virtual DbSet<TB_Capstone_AREA> TB_Capstone_AREA { get; set; }
        public virtual DbSet<TB_Capstone_DEPARTMENT> TB_Capstone_DEPARTMENT { get; set; }
        public virtual DbSet<TB_Capstone_EMPLOYEE> TB_Capstone_EMPLOYEE { get; set; }
        public virtual DbSet<TB_Capstone_ENTITLED_TIME_OFF> TB_Capstone_ENTITLED_TIME_OFF { get; set; }
        public virtual DbSet<TB_Capstone_OFFDAY_TYPE> TB_Capstone_OFFDAY_TYPE { get; set; }
        public virtual DbSet<TB_Capstone_OVERTIME> TB_Capstone_OVERTIME { get; set; }
        public virtual DbSet<TB_Capstone_OVERTIME_TYPE> TB_Capstone_OVERTIME_TYPE { get; set; }
        public virtual DbSet<TB_Capstone_PAID_HOLIDAY> TB_Capstone_PAID_HOLIDAY { get; set; }
        public virtual DbSet<TB_Capstone_POSITION> TB_Capstone_POSITION { get; set; }
        public virtual DbSet<TB_Capstone_PROJECT> TB_Capstone_PROJECT { get; set; }
        public virtual DbSet<TB_Capstone_PROJECT_CATEGORY> TB_Capstone_PROJECT_CATEGORY { get; set; }
        public virtual DbSet<TB_Capstone_PROJECT_DETAIL> TB_Capstone_PROJECT_DETAIL { get; set; }
        public virtual DbSet<TB_Capstone_ROLE> TB_Capstone_ROLE { get; set; }
        public virtual DbSet<TB_Capstone_SCHEDULE_TYPE> TB_Capstone_SCHEDULE_TYPE { get; set; }
        public virtual DbSet<TB_Capstone_SCHEDULE_TYPE_DETAILS> TB_Capstone_SCHEDULE_TYPE_DETAILS { get; set; }
        public virtual DbSet<TB_Capstone_TEAM> TB_Capstone_TEAM { get; set; }
        public virtual DbSet<TB_Capstone_TEAM_HISTORY> TB_Capstone_TEAM_HISTORY { get; set; }
        public virtual DbSet<TB_Capstone_UNIT> TB_Capstone_UNIT { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TB_Capstone_ABSENCE_DETAIL>()
                .Property(e => e.HalfDay)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_ABSENCE_DETAIL>()
                .Property(e => e.Hours)
                .HasPrecision(5, 2);

            modelBuilder.Entity<TB_Capstone_ABSENCE_DETAIL>()
                .Property(e => e.Notes)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_ALLOCATION>()
                .Property(e => e.AllocatedDays)
                .HasPrecision(4, 1);

            modelBuilder.Entity<TB_Capstone_ALLOCATION>()
                .Property(e => e.January)
                .HasPrecision(3, 1);

            modelBuilder.Entity<TB_Capstone_ALLOCATION>()
                .Property(e => e.February)
                .HasPrecision(3, 1);

            modelBuilder.Entity<TB_Capstone_ALLOCATION>()
                .Property(e => e.March)
                .HasPrecision(3, 1);

            modelBuilder.Entity<TB_Capstone_ALLOCATION>()
                .Property(e => e.April)
                .HasPrecision(3, 1);

            modelBuilder.Entity<TB_Capstone_ALLOCATION>()
                .Property(e => e.May)
                .HasPrecision(3, 1);

            modelBuilder.Entity<TB_Capstone_ALLOCATION>()
                .Property(e => e.June)
                .HasPrecision(3, 1);

            modelBuilder.Entity<TB_Capstone_ALLOCATION>()
                .Property(e => e.July)
                .HasPrecision(3, 1);

            modelBuilder.Entity<TB_Capstone_ALLOCATION>()
                .Property(e => e.August)
                .HasPrecision(3, 1);

            modelBuilder.Entity<TB_Capstone_ALLOCATION>()
                .Property(e => e.September)
                .HasPrecision(3, 1);

            modelBuilder.Entity<TB_Capstone_ALLOCATION>()
                .Property(e => e.October)
                .HasPrecision(3, 1);

            modelBuilder.Entity<TB_Capstone_ALLOCATION>()
                .Property(e => e.November)
                .HasPrecision(3, 1);

            modelBuilder.Entity<TB_Capstone_ALLOCATION>()
                .Property(e => e.December)
                .HasPrecision(3, 1);

            modelBuilder.Entity<TB_Capstone_ALLOCATION>()
                .Property(e => e.Notes)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_AREA>()
                .Property(e => e.AreaName)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_AREA>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_AREA>()
                .HasMany(e => e.TB_Capstone_UNIT)
                .WithRequired(e => e.TB_Capstone_AREA)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Capstone_DEPARTMENT>()
                .Property(e => e.DepartmentName)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_DEPARTMENT>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_DEPARTMENT>()
                .HasMany(e => e.TB_Capstone_AREA)
                .WithRequired(e => e.TB_Capstone_DEPARTMENT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
                .Property(e => e.AlternatePhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
                .Property(e => e.CompanyPhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
                .Property(e => e.EmergencyContactName1)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
                .Property(e => e.EmergencyContactPhoneNumber1)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
                .Property(e => e.EmergencyContactName2)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
                .Property(e => e.EmergencyContactPhoneNumber2)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
                .HasMany(e => e.TB_Capstone_ABSENCE_DETAIL)
                .WithRequired(e => e.TB_Capstone_EMPLOYEE)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
                .HasMany(e => e.TB_Capstone_ABSENCE_DETAIL)
                .WithRequired(e => e.TB_Capstone_EMPLOYEE)
                .HasForeignKey(e => e.EmployeeID)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
            //    .HasMany(e => e.TB_Capstone_ABSENCE_DETAIL2)
            //    .WithOptional(e => e.TB_Capstone_EMPLOYEE2)
            //    .HasForeignKey(e => e.UpdatedBy);

            modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
                .HasMany(e => e.TB_Capstone_ALLOCATION)
                .WithRequired(e => e.TB_Capstone_EMPLOYEE)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
                .HasMany(e => e.TB_Capstone_ALLOCATION)
                .WithRequired(e => e.TB_Capstone_EMPLOYEE)
                .HasForeignKey(e => e.EmployeeID)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
            //    .HasMany(e => e.TB_Capstone_ALLOCATION2)
            //    .WithOptional(e => e.TB_Capstone_EMPLOYEE2)
            //    .HasForeignKey(e => e.UpdatedBy);

            modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
                .HasMany(e => e.TB_Capstone_AREA)
                .WithRequired(e => e.TB_Capstone_EMPLOYEE)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
            //    .HasMany(e => e.TB_Capstone_AREA1)
            //    .WithOptional(e => e.TB_Capstone_EMPLOYEE1)
            //    .HasForeignKey(e => e.UpdatedBy);

            modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
                .HasMany(e => e.TB_Capstone_DEPARTMENT)
                .WithRequired(e => e.TB_Capstone_EMPLOYEE)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
            //    .HasMany(e => e.TB_Capstone_DEPARTMENT1)
            //    .WithOptional(e => e.TB_Capstone_EMPLOYEE1)
            //    .HasForeignKey(e => e.UpdatedBy);

            modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
                .HasMany(e => e.TB_Capstone_ENTITLED_TIME_OFF)
                .WithRequired(e => e.TB_Capstone_EMPLOYEE)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
                .HasMany(e => e.TB_Capstone_ENTITLED_TIME_OFF)
                .WithRequired(e => e.TB_Capstone_EMPLOYEE)
                .HasForeignKey(e => e.EmployeeID)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
            //    .HasMany(e => e.TB_Capstone_ENTITLED_TIME_OFF2)
            //    .WithOptional(e => e.TB_Capstone_EMPLOYEE2)
            //    .HasForeignKey(e => e.UpdatedBy);

            modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
                .HasMany(e => e.TB_Capstone_OVERTIME_TYPE)
                .WithRequired(e => e.TB_Capstone_EMPLOYEE)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
                .HasMany(e => e.TB_Capstone_OVERTIME)
                .WithRequired(e => e.TB_Capstone_EMPLOYEE)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
                .HasMany(e => e.TB_Capstone_OFFDAY_TYPE)
                .WithRequired(e => e.TB_Capstone_EMPLOYEE)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
                .HasMany(e => e.TB_Capstone_OVERTIME)
                .WithRequired(e => e.TB_Capstone_EMPLOYEE)
                .HasForeignKey(e => e.EmployeeID)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
            //    .HasMany(e => e.TB_Capstone_OVERTIME_TYPE1)
            //    .WithOptional(e => e.TB_Capstone_EMPLOYEE1)
            //    .HasForeignKey(e => e.UpdatedBy);

            //modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
            //    .HasMany(e => e.TB_Capstone_OVERTIME2)
            //    .WithOptional(e => e.TB_Capstone_EMPLOYEE2)
            //    .HasForeignKey(e => e.UpdatedBy);

            //modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
            //    .HasMany(e => e.TB_Capstone_OFFDAY_TYPE1)
            //    .WithOptional(e => e.TB_Capstone_EMPLOYEE1)
            //    .HasForeignKey(e => e.UpdatedBy);

            modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
                .HasMany(e => e.TB_Capstone_PROJECT_CATEGORY)
                .WithRequired(e => e.TB_Capstone_EMPLOYEE)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
                .HasMany(e => e.TB_Capstone_PROJECT)
                .WithRequired(e => e.TB_Capstone_EMPLOYEE)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
                .HasMany(e => e.TB_Capstone_PROJECT_DETAIL)
                .WithRequired(e => e.TB_Capstone_EMPLOYEE)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
                .HasMany(e => e.TB_Capstone_PAID_HOLIDAY)
                .WithRequired(e => e.TB_Capstone_EMPLOYEE)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
                .HasMany(e => e.TB_Capstone_PROJECT_DETAIL)
                .WithRequired(e => e.TB_Capstone_EMPLOYEE)
                .HasForeignKey(e => e.EmployeeID)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
            //    .HasMany(e => e.TB_Capstone_PROJECT_CATEGORY1)
            //    .WithOptional(e => e.TB_Capstone_EMPLOYEE1)
            //    .HasForeignKey(e => e.UpdatedBy);

            //modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
            //    .HasMany(e => e.TB_Capstone_PROJECT1)
            //    .WithOptional(e => e.TB_Capstone_EMPLOYEE1)
            //    .HasForeignKey(e => e.UpdatedBy);

            //modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
            //    .HasMany(e => e.TB_Capstone_PROJECT_DETAIL2)
            //    .WithOptional(e => e.TB_Capstone_EMPLOYEE2)
            //    .HasForeignKey(e => e.UpdatedBy);

            //modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
            //    .HasMany(e => e.TB_Capstone_PAID_HOLIDAY1)
            //    .WithOptional(e => e.TB_Capstone_EMPLOYEE1)
            //    .HasForeignKey(e => e.UpdatedBy);

            modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
                .HasMany(e => e.TB_Capstone_SCHEDULE_TYPE_DETAILS)
                .WithRequired(e => e.TB_Capstone_EMPLOYEE)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
                .HasMany(e => e.TB_Capstone_SCHEDULE_TYPE)
                .WithRequired(e => e.TB_Capstone_EMPLOYEE)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
                .HasMany(e => e.TB_Capstone_SCHEDULE_TYPE_DETAILS)
                .WithRequired(e => e.TB_Capstone_EMPLOYEE)
                .HasForeignKey(e => e.EmployeeID)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
            //    .HasMany(e => e.TB_Capstone_SCHEDULE_TYPE_DETAILS2)
            //    .WithOptional(e => e.TB_Capstone_EMPLOYEE2)
            //    .HasForeignKey(e => e.UpdatedBy);

            //modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
            //    .HasMany(e => e.TB_Capstone_SCHEDULE_TYPE1)
            //    .WithOptional(e => e.TB_Capstone_EMPLOYEE1)
            //    .HasForeignKey(e => e.UpdatedBy);

            modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
                .HasMany(e => e.TB_Capstone_TEAM_HISTORY)
                .WithRequired(e => e.TB_Capstone_EMPLOYEE)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
                .HasMany(e => e.TB_Capstone_TEAM_HISTORY)
                .WithRequired(e => e.TB_Capstone_EMPLOYEE)
                .HasForeignKey(e => e.EmployeeID)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
            //    .HasMany(e => e.TB_Capstone_TEAM_HISTORY2)
            //    .WithOptional(e => e.TB_Capstone_EMPLOYEE2)
            //    .HasForeignKey(e => e.UpdatedBy);

            //modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
            //    .HasMany(e => e.TB_Capstone_POSITION1)
            //    .WithRequired(e => e.TB_Capstone_EMPLOYEE1)
            //    .HasForeignKey(e => e.CreatedBy)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
            //    .HasMany(e => e.TB_Capstone_ROLE1)
            //    .WithRequired(e => e.TB_Capstone_EMPLOYEE1)
            //    .HasForeignKey(e => e.CreatedBy)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
                .HasMany(e => e.TB_Capstone_TEAM)
                .WithRequired(e => e.TB_Capstone_EMPLOYEE)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
            //    .HasMany(e => e.TB_Capstone_TEAM_HISTORY3)
            //    .WithRequired(e => e.TB_Capstone_EMPLOYEE3)
            //    .HasForeignKey(e => e.CreatedBy)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
                .HasMany(e => e.TB_Capstone_UNIT)
                .WithRequired(e => e.TB_Capstone_EMPLOYEE)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
            //    .HasMany(e => e.TB_Capstone_EMPLOYEE1)
            //    .WithOptional(e => e.TB_Capstone_EMPLOYEE2)
            //    .HasForeignKey(e => e.UpdatedBy);

            //modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
            //    .HasMany(e => e.TB_Capstone_POSITION2)
            //    .WithOptional(e => e.TB_Capstone_EMPLOYEE2)
            //    .HasForeignKey(e => e.UpdatedBy);

            //modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
            //    .HasMany(e => e.TB_Capstone_ROLE2)
            //    .WithOptional(e => e.TB_Capstone_EMPLOYEE2)
            //    .HasForeignKey(e => e.UpdatedBy);

            //modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
            //    .HasMany(e => e.TB_Capstone_TEAM1)
            //    .WithOptional(e => e.TB_Capstone_EMPLOYEE1)
            //    .HasForeignKey(e => e.UpdatedBy);

            //modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
            //    .HasMany(e => e.TB_Capstone_TEAM_HISTORY4)
            //    .WithOptional(e => e.TB_Capstone_EMPLOYEE4)
            //    .HasForeignKey(e => e.UpdatedBy);

            //modelBuilder.Entity<TB_Capstone_EMPLOYEE>()
            //    .HasMany(e => e.TB_Capstone_UNIT1)
            //    .WithOptional(e => e.TB_Capstone_EMPLOYEE1)
            //    .HasForeignKey(e => e.UpdatedBy);

            modelBuilder.Entity<TB_Capstone_ENTITLED_TIME_OFF>()
                .Property(e => e.HoursAccumulated)
                .HasPrecision(8, 2);

            modelBuilder.Entity<TB_Capstone_OFFDAY_TYPE>()
                .Property(e => e.AbbreviatedName)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_OFFDAY_TYPE>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_OFFDAY_TYPE>()
                .Property(e => e.Notes)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_OFFDAY_TYPE>()
                .Property(e => e.Color)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_OFFDAY_TYPE>()
                .HasMany(e => e.TB_Capstone_ABSENCE_DETAIL)
                .WithRequired(e => e.TB_Capstone_OFFDAY_TYPE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Capstone_OFFDAY_TYPE>()
                .HasMany(e => e.TB_Capstone_ENTITLED_TIME_OFF)
                .WithRequired(e => e.TB_Capstone_OFFDAY_TYPE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Capstone_OVERTIME>()
                .Property(e => e.Amount)
                .HasPrecision(5, 2);

            modelBuilder.Entity<TB_Capstone_OVERTIME>()
                .Property(e => e.SubmissionNotes)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_OVERTIME>()
                .Property(e => e.ApprovalNotes)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_OVERTIME>()
                .Property(e => e.Approved)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_OVERTIME_TYPE>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_OVERTIME_TYPE>()
                .Property(e => e.PayMultiplier)
                .HasPrecision(3, 2);

            modelBuilder.Entity<TB_Capstone_OVERTIME_TYPE>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_OVERTIME_TYPE>()
                .HasMany(e => e.TB_Capstone_OVERTIME)
                .WithRequired(e => e.TB_Capstone_OVERTIME_TYPE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Capstone_PAID_HOLIDAY>()
                .Property(e => e.HolidayName)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_PAID_HOLIDAY>()
                .Property(e => e.Notes)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_POSITION>()
                .Property(e => e.PositionTitle)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_POSITION>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_POSITION>()
                .HasMany(e => e.TB_Capstone_EMPLOYEE)
                .WithRequired(e => e.TB_Capstone_POSITION)
                .HasForeignKey(e => e.PositionID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Capstone_PROJECT>()
                .Property(e => e.ProjectName)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_PROJECT>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_PROJECT>()
                .HasMany(e => e.TB_Capstone_ALLOCATION)
                .WithRequired(e => e.TB_Capstone_PROJECT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Capstone_PROJECT>()
                .HasMany(e => e.TB_Capstone_PROJECT_DETAIL)
                .WithRequired(e => e.TB_Capstone_PROJECT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Capstone_PROJECT_CATEGORY>()
                .Property(e => e.CategoryName)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_PROJECT_CATEGORY>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_PROJECT_CATEGORY>()
                .Property(e => e.Color)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_PROJECT_CATEGORY>()
                .HasMany(e => e.TB_Capstone_PROJECT)
                .WithRequired(e => e.TB_Capstone_PROJECT_CATEGORY)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Capstone_PROJECT_DETAIL>()
                .Property(e => e.Notes)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_ROLE>()
                .Property(e => e.RoleTitle)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_ROLE>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_ROLE>()
                .HasMany(e => e.TB_Capstone_TEAM_HISTORY)
                .WithRequired(e => e.TB_Capstone_ROLE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Capstone_SCHEDULE_TYPE>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_SCHEDULE_TYPE>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_SCHEDULE_TYPE>()
                .Property(e => e.HoursPerDay)
                .HasPrecision(5, 2);

            modelBuilder.Entity<TB_Capstone_SCHEDULE_TYPE>()
                .HasMany(e => e.TB_Capstone_SCHEDULE_TYPE_DETAILS)
                .WithRequired(e => e.TB_Capstone_SCHEDULE_TYPE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Capstone_TEAM>()
                .Property(e => e.TeamName)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_TEAM>()
                .HasMany(e => e.TB_Capstone_TEAM_HISTORY)
                .WithRequired(e => e.TB_Capstone_TEAM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TB_Capstone_UNIT>()
                .Property(e => e.UnitName)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_UNIT>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<TB_Capstone_UNIT>()
                .HasMany(e => e.TB_Capstone_TEAM)
                .WithRequired(e => e.TB_Capstone_UNIT)
                .WillCascadeOnDelete(false);
        }
    }
}
