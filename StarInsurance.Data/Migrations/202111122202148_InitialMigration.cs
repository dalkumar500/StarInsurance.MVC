namespace StarInsurance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customer", "CreatedUtc", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.Customer", "ModifiedUtc", c => c.DateTimeOffset(precision: 7));
            AddColumn("dbo.InsurancePolicy", "CreatedUtc", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.InsurancePolicy", "ModifiedUtc", c => c.DateTimeOffset(precision: 7));
            AddColumn("dbo.PolicyClaim", "ModifiedUtc", c => c.DateTimeOffset(precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PolicyClaim", "ModifiedUtc");
            DropColumn("dbo.InsurancePolicy", "ModifiedUtc");
            DropColumn("dbo.InsurancePolicy", "CreatedUtc");
            DropColumn("dbo.Customer", "ModifiedUtc");
            DropColumn("dbo.Customer", "CreatedUtc");
        }
    }
}
