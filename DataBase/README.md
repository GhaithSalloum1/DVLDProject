## 💾 Database Setup

This project uses **Microsoft SQL Server**. You can set up the database using either of the following methods located in the `Database/` folder:

### Option 1: Execute SQL Script (Recommended)
1. Open SQL Server Management Studio (SSMS).
2. Open `Database/DVLD_Full_Script.sql`.
3. Execute the script to generate all tables, relationships, sample data, and views.

### Option 2: Restore Database Backup
1. Copy `Database/DVLD_Backup.bak` to your local SQL Server backup location.
2. In SSMS, right-click **Databases** -> **Restore Database**.
3. Select **Device**, locate `DVLD_Backup.bak`, and complete the restoration.
4. If you hit permission issues with the restored database, run:
   ```sql
   ALTER AUTHORIZATION ON DATABASE::DVLD TO [DOMAIN\YourUsername];
