namespace PrescriptionDataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPrescriptionController : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PrescriptionModels", "PrescriberId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PrescriptionModels", "PrescriberId");
        }
    }
}
