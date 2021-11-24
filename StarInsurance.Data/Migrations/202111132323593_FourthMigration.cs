namespace StarInsurance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FourthMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customer", "Email", c => c.String(nullable: false));
            AddColumn("dbo.InsurancePolicy", "PolicyNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InsurancePolicy", "PolicyNumber");
            DropColumn("dbo.Customer", "Email");
        }
    }
}
