namespace PrescriptionDataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDbToDefaultConnection : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientModels", "DOB", c => c.DateTime(nullable: false));
            DropColumn("dbo.PatientModels", "Age");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PatientModels", "Age", c => c.Int(nullable: false));
            DropColumn("dbo.PatientModels", "DOB");
        }
    }
}
