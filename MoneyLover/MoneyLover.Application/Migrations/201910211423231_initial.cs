namespace MoneyLover.Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Banks",
                c => new
                    {
                        BankID = c.Int(nullable: false, identity: true),
                        BankName = c.String(),
                        ShortName = c.String(),
                    })
                .PrimaryKey(t => t.BankID);
            
            CreateTable(
                "dbo.PassBooks",
                c => new
                    {
                        PassBookID = c.Int(nullable: false, identity: true),
                        BankID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        SentDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Deposit = c.Double(nullable: false),
                        Term = c.Int(nullable: false),
                        InterestRates = c.Double(nullable: false),
                        IndefiniteTerm = c.Double(nullable: false),
                        PayInterest = c.Int(nullable: false),
                        Due = c.Int(nullable: false),
                        WithDrawalMoney = c.Double(nullable: false),
                        Settlement = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PassBookID)
                .ForeignKey("dbo.Banks", t => t.BankID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.BankID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                        Wallet = c.Double(nullable: false),
                        SavingsWallet = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PassBooks", "UserID", "dbo.Users");
            DropForeignKey("dbo.PassBooks", "BankID", "dbo.Banks");
            DropIndex("dbo.PassBooks", new[] { "UserID" });
            DropIndex("dbo.PassBooks", new[] { "BankID" });
            DropTable("dbo.Users");
            DropTable("dbo.PassBooks");
            DropTable("dbo.Banks");
        }
    }
}
