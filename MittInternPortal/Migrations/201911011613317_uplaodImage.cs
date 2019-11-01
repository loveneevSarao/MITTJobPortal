namespace MittInternPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uplaodImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Instructors", "InstructorImagePath", c => c.String());
            AddColumn("dbo.Students", "StudentImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "StudentImagePath");
            DropColumn("dbo.Instructors", "InstructorImagePath");
        }
    }
}
