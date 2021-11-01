# Automated Environment Controller
A physical controller which keeps track of environmental data around itself. 
The data collected by the Data Logging Application (DLA) is stored in a shared database, 
which then can be accessed by the Data Monitoring Application (DMoA).

Configuration values for the sensors can be changes and controlled through the 
Data Management Application (DMaA), which is based on a .NET-framework GUI. This GUI is
also being used by the DLA out of practicality reasons. 

The DMoA is based on Razor-page through ASP.NET.  