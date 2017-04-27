namespace Menhely.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Allats",
                c => new
                    {
                        Nev = c.String(nullable: false, maxLength: 128),
                        Leiras = c.String(),
                        Kor = c.Int(nullable: false),
                        Faj = c.Int(nullable: false),
                        AlFaj = c.String(),
                        Allapot = c.Int(nullable: false),
                        Ketrec_KetrecID = c.Int(),
                        Orokbefogado_Nev = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Nev)
                .ForeignKey("dbo.Ketrecs", t => t.Ketrec_KetrecID)
                .ForeignKey("dbo.Orokbefogadoes", t => t.Orokbefogado_Nev)
                .Index(t => t.Ketrec_KetrecID)
                .Index(t => t.Orokbefogado_Nev);
            
            CreateTable(
                "dbo.Gondozoes",
                c => new
                    {
                        Nev = c.String(nullable: false, maxLength: 128),
                        Jelszo = c.String(),
                        Beosztas = c.Int(nullable: false),
                        UtolsoCselekves = c.DateTime(nullable: false),
                        Bejelentkezhet = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Nev);
            
            CreateTable(
                "dbo.Telephelies",
                c => new
                    {
                        Cim = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Cim);
            
            CreateTable(
                "dbo.Ketrecs",
                c => new
                    {
                        KetrecID = c.Int(nullable: false, identity: true),
                        Meret = c.Int(nullable: false),
                        Faj = c.Int(nullable: false),
                        Hely_Cim = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.KetrecID)
                .ForeignKey("dbo.Telephelies", t => t.Hely_Cim)
                .Index(t => t.Hely_Cim);
            
            CreateTable(
                "dbo.Orokbefogadoes",
                c => new
                    {
                        Nev = c.String(nullable: false, maxLength: 128),
                        Jelszo = c.String(),
                        Adomany = c.Int(nullable: false),
                        UtolsoCselekves = c.DateTime(nullable: false),
                        Bejelentkezhet = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Nev);
            
            CreateTable(
                "dbo.GondozoAllats",
                c => new
                    {
                        Gondozo_Nev = c.String(nullable: false, maxLength: 128),
                        Allat_Nev = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Gondozo_Nev, t.Allat_Nev })
                .ForeignKey("dbo.Gondozoes", t => t.Gondozo_Nev, cascadeDelete: true)
                .ForeignKey("dbo.Allats", t => t.Allat_Nev, cascadeDelete: true)
                .Index(t => t.Gondozo_Nev)
                .Index(t => t.Allat_Nev);
            
            CreateTable(
                "dbo.TelephelyGondozoes",
                c => new
                    {
                        Telephely_Cim = c.String(nullable: false, maxLength: 128),
                        Gondozo_Nev = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Telephely_Cim, t.Gondozo_Nev })
                .ForeignKey("dbo.Telephelies", t => t.Telephely_Cim, cascadeDelete: true)
                .ForeignKey("dbo.Gondozoes", t => t.Gondozo_Nev, cascadeDelete: true)
                .Index(t => t.Telephely_Cim)
                .Index(t => t.Gondozo_Nev);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Allats", "Orokbefogado_Nev", "dbo.Orokbefogadoes");
            DropForeignKey("dbo.Ketrecs", "Hely_Cim", "dbo.Telephelies");
            DropForeignKey("dbo.Allats", "Ketrec_KetrecID", "dbo.Ketrecs");
            DropForeignKey("dbo.TelephelyGondozoes", "Gondozo_Nev", "dbo.Gondozoes");
            DropForeignKey("dbo.TelephelyGondozoes", "Telephely_Cim", "dbo.Telephelies");
            DropForeignKey("dbo.GondozoAllats", "Allat_Nev", "dbo.Allats");
            DropForeignKey("dbo.GondozoAllats", "Gondozo_Nev", "dbo.Gondozoes");
            DropIndex("dbo.TelephelyGondozoes", new[] { "Gondozo_Nev" });
            DropIndex("dbo.TelephelyGondozoes", new[] { "Telephely_Cim" });
            DropIndex("dbo.GondozoAllats", new[] { "Allat_Nev" });
            DropIndex("dbo.GondozoAllats", new[] { "Gondozo_Nev" });
            DropIndex("dbo.Ketrecs", new[] { "Hely_Cim" });
            DropIndex("dbo.Allats", new[] { "Orokbefogado_Nev" });
            DropIndex("dbo.Allats", new[] { "Ketrec_KetrecID" });
            DropTable("dbo.TelephelyGondozoes");
            DropTable("dbo.GondozoAllats");
            DropTable("dbo.Orokbefogadoes");
            DropTable("dbo.Ketrecs");
            DropTable("dbo.Telephelies");
            DropTable("dbo.Gondozoes");
            DropTable("dbo.Allats");
        }
    }
}
