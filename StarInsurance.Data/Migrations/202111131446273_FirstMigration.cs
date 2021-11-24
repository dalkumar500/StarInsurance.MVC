namespace StarInsurance.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PolicyClaim", "InsurancePolicyId", "dbo.InsurancePolicy");
            DropIndex("dbo.PolicyClaim", new[] { "InsurancePolicyId" });
            AlterColumn("dbo.PolicyClaim", "InsurancePolicyId", c => c.Int());
            CreateIndex("dbo.PolicyClaim", "InsurancePolicyId");
            AddForeignKey("dbo.PolicyClaim", "InsurancePolicyId", "dbo.InsurancePolicy", "InsurancePolicyId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PolicyClaim", "InsurancePolicyId", "dbo.InsurancePolicy");
            DropIndex("dbo.PolicyClaim", new[] { "InsurancePolicyId" });
            AlterColumn("dbo.PolicyClaim", "InsurancePolicyId", c => c.Int(nullable: false));
            CreateIndex("dbo.PolicyClaim", "InsurancePolicyId");
            AddForeignKey("dbo.PolicyClaim", "InsurancePolicyId", "dbo.InsurancePolicy", "InsurancePolicyId", cascadeDelete: true);
        }
    }
}
