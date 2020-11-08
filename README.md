# BankJoy-Assignment

Points to note related to the assignment:

While this assignment deals with the implementation of Bannking services, and an attempt has been made to archict some part of the code (with limited time and scope), 
this by no means the end of possibilities that can go into creating a real application. Following things have been ignored for brevity:

1) Logging (Serilog package has been added but logging is not implemented)
2) Core domain entity has been used across Web layer (instead of creating a concise data transfer object and mapping the domain object to DTO object), 
   because the schema provided is too simple for the need of DTO.
3) Fluent validation has been used for validation but not exhaustively (due to time constraints)
4) A dynamic method of registering services has been implemented to show that it can come handy as the application is extended and does support plugin architecture as well.
5) Core features like "Transfer Money from one member account to another member account" can be effectively implemented with State Pattern 
  (Empty classes have been created to show case that)
6) HATEOS principle has not been applied to REST end points.
7) Trasactions are processed synchronously in this assignment and transaction log is not saved (due to time constraints). I believe the core part of banking application is to process the trasactions with a reliable, robust and scalable Message Broker softwares (like kafka), so that the order and singulairty of transaction is maintained. Though I do not have working expereince with Event-Sourcing pattern, I feel Event Sourcing pattern is  well suited for abcnking services, becasue the trail of activity of transactions is very imperative.
8) The test cases are not exhaustive. I can add (and improve it) from what we have so far.

