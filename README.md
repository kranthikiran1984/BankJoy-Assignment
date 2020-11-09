# BankJoy-Assignment

Points to note related to the assignment:

While this assignment deals with the implementation of Bannking services, and an attempt has been made to archict some part of the code (with limited time and scope), 
this by no means the end of possibilities that can go into creating a real application. Following things have been ignored for brevity:

1) My laptop has .Net Core 2.2 configured, so the project is the set to use that instead of 3.0
2) I ahve slighlt modified the schema ('Id' instead of 'InstitutionId' etc), so that it fits well with ORM package I have used for Json database.
3) Logging (Serilog package has been added but logging is not implemented)
4) Core domain entity has been used across Web layer (instead of creating a concise data transfer object and mapping the domain object to DTO object), 
   because the schema provided is too simple for the need of DTO.
5) Fluent validation has been used for validation, but not through out all  methods (due to time constraints)
6) A dynamic method of registering services has been implemented to show that it can come handy as the application is extended and does support plugin architecture as well.
7) Core features like "Transfer Money from one member account to another member account" can be effectively implemented with State Pattern 
  (Empty classes have been created to show case that)
8) HATEOS principle has not been applied to REST end points.
9) Trasactions are processed synchronously in this assignment and transaction log is not saved (due to time constraints). I believe the core part of banking application is to process the trasactions with a reliable, robust and scalable Message Broker softwares (like kafka), so that the order and singulairty of transaction is maintained. Though I do not have working expereince with Event-Sourcing pattern, I feel Event Sourcing pattern is  well suited for abcnking services, becasue the trail of activity of transactions is very imperative.
10) Some sample events are created and published just to demonstrate how events can be useful design instead of cluttering unrelated code in a method.
11) The test cases are not exhaustive. I can add (and improve it) from what we have so far.
12) Need to install Swagger (SwashBuckle Package for .net). 
13) Lastly the application, at this point, does build and most likely won't work (after changes that have been done after a stable point). 

At the moment the code is a just a representation on design and path it is moving towards to make it work. I am happy to extend it further on any aspects you are interested to see, fix the issues and make it work. I can spend time again after wednesday 18th october.

Thank you!
