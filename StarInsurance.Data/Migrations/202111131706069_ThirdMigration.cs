namespace StarInsurance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirdMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customer", "InsurancePolicyId", "dbo.InsurancePolicy");
            DropIndex("dbo.Customer", new[] { "InsurancePolicyId" });
            AlterColumn("dbo.Customer", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Customer", "InsurancePolicyId", c => c.Int());
            CreateIndex("dbo.Customer", "InsurancePolicyId");
            AddForeignKey("dbo.Customer", "InsurancePolicyId", "dbo.InsurancePolicy", "InsurancePolicyId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customer", "InsurancePolicyId", "dbo.InsurancePolicy");
            DropIndex("dbo.Customer", new[] { "InsurancePolicyId" });
            AlterColumn("dbo.Customer", "InsurancePolicyId", c => c.Int(nullable: false));
            AlterColumn("dbo.Customer", "Address", c => c.String());
            CreateIndex("dbo.Customer", "InsurancePolicyId");
            AddForeignKey("dbo.Customer", "InsurancePolicyId", "dbo.InsurancePolicy", "InsurancePolicyId", cascadeDelete: true);
        }
    }
}
