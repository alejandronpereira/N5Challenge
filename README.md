# How to run it?

1) Open Powershell or a terminal as admin and go to the path where the docker-compose.yml is located. Once you're located there run the command "docker-compose -f docker-compose.yml up" and wait a few minutes until the containers are up (for sure you must have docker installed in your machine).

   ![image](https://github.com/alejandronpereira/N5Challenge/assets/6674605/6702e393-a0da-4b4c-9568-3d2ff26d9bfe)

   ![image](https://github.com/alejandronpereira/N5Challenge/assets/6674605/3326a019-28f6-4115-b15c-0918a24df932)



2) Once that the containers were build, open the solution in Visual Studio and run it with the WebApi as startup project. By default 3 PermissionTypes are created: Admin (1), Employee (2) and Contractor (3). And also three random Permissions are created.

 ![image](https://github.com/alejandronpereira/N5Challenge/assets/6674605/d1eff5e2-93fa-4a74-8d07-fe0768941216)

 ![image](https://github.com/alejandronpereira/N5Challenge/assets/6674605/ec64ecb5-548e-48f4-8f4d-ed3002a55d58)
 


3) If we want to check something related with ElasticSearch we can go to the url localhost:9200/permissions/_docs/{id} in the browser (since the database was created with 3 Permissions already, we can see here all the new permissions that are created, from the id number 4 on. If we use the 1,2 or 3 will not retrieve anything)

![image](https://github.com/alejandronpereira/N5Challenge/assets/6674605/67f4fc6b-394b-4c15-818f-d4345c583b61)



4) If we want to check the requests we are doing we can see the "testtopic" that was created in Kafka for that purpose. One way to see the values in the topic could be downloading the Offset Explorer ((https://www.kafkatool.com/download.html) and attach the kafka instance to it. The way we can see the messages it's like this:
   
  a) Add a new connection in the OE.

  b) Put a name to the Cluster in the "Properties" tab and then go to the "Advanced" tab and put the instance of the Kafka we are using with docker (localhost:9092).
  
  c) Once that the connection was setted up, go to the topic and from the "Properties" tab change the value from Byte Array to String and make click in the Update button.
  
  d) After we ran some enpoints, we'll be able to see the messages coming in the partition of the topic behind the column "Value".

![image](https://github.com/alejandronpereira/N5Challenge/assets/6674605/15d47aba-8494-4e99-9c2f-add85d932f2c)
![image](https://github.com/alejandronpereira/N5Challenge/assets/6674605/833a689a-8464-4f35-861b-c1fa6c4d1090)
![image](https://github.com/alejandronpereira/N5Challenge/assets/6674605/da43586f-0856-4654-995c-b2733adc197e)
![image](https://github.com/alejandronpereira/N5Challenge/assets/6674605/af2cb5ae-7f62-414c-9d50-6b86bb85a3e2)
![image](https://github.com/alejandronpereira/N5Challenge/assets/6674605/ed78b512-a8a7-4f1e-bb9e-88e0698877bb)





