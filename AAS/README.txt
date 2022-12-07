IMPORTANT: This application only works on windows!
Due to problems with the local SQL database within visual studio on mac.

AAS installation instructions
Prerequisites
Visual studio must have the correct workload
1.	Search and open “Visual Studio Installer” in the Windows start menu.
2.	Modify your existing installation of Visual Studio.
3.	Check that Visual studio has the “ASP.NET and web development workload” installed.
4.	If not select and install this workload.

Installation
1.	Open visual studio
2.	Clone the repository using the URL https://github.com/OHeckIsThatSam/AAS.git
3.	Or open the submitted files
4.	Run the program. IT WILL CRASH. Press yes on all popups. This is to sync the dependencies of the project. Stop the project when it crashes.
5.	Go to Tools->NuGet Package Manager->Package Manager Console.
6.	Run the command “Update-Database” this will create the database from the given initial migration.
7.	Next in the Project Solution Explorer open the folder “DatabaseTriggers”
8.	Open the file “BalanceUpdateTrigger.sql” and click the Execute in the top left of the window.
9.	Then select Local, then “MSSQLLocalDB” from the drop down. THIS OPTION DOES NOT APPEAR ON MAC OR IF THE CORRECT WORKLOAD WAS NOT INSTALLED. See Prerequisites.
10.	For the Database Name drop down box select AAS.Data
11.	Then Connect
12.	Repeat from step 8 with the file “SalaryUpdateTrigger.sql”
13.	Run the program. You are all good to go!
