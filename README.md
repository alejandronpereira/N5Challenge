# How to run it?

1) Open Powershell or a terminal as admin and go to the path where the docker-compose.yml is located. Once you're located there run the command "docker-compose -f docker-compose.yml up" and wait a few minutes until the containers are up (for sure you must have docker installed in your machine).

   ![image](https://github.com/alejandronpereira/N5Challenge/assets/6674605/92c771e7-1d73-4460-818d-ff40820685c0)

   ![image](https://github.com/alejandronpereira/N5Challenge/assets/6674605/49519ace-2b88-4e7b-8bd1-af5f9992b0da)


2) Once that the containers were build, open the solution in Visual Studio and run it with the WebApi as startup project.

 ![image](https://github.com/alejandronpereira/N5Challenge/assets/6674605/10e2c135-2018-489c-b7f5-ada546798093)

 ![image](https://github.com/alejandronpereira/N5Challenge/assets/6674605/b7d72aaf-739c-4b1b-83ac-cfb2cf5e2e5f)


By default 3 PermissionTypes are created: Admin (1), Employee (2) and Contractor (3). And also three random Permissions are created.

3) If we want to check something related with ElasticSearch we can go to the url localhost:9200/permissions/_docs/{id} in the browser (since the database was created with 3 Permissions already, we can see here all the new permissions that are created, from the id number 4 on. If we use the 1,2 or 3 will not retrieve anything)

![image](https://github.com/alejandronpereira/N5Challenge/assets/6674605/c3c8f599-8300-42ef-b8f7-80f2f547d2e3)

4) If we want to check the requests we are doing we can see the "testtopic" that was created in Kafka for that purpose. One way to see the values in the topic could be downloading the Offset Explorer ((https://www.kafkatool.com/download.html) and attach the kafka instance to it. The way we can see the messages it's like this:
   
  a) Add a new connection in the OE.

  b) Put a name to the Cluster in the "Properties" tab and then go to the "Advanced" tab and put the instance of the Kafka we are using with docker (localhost:9092).
  
  c) Once that the connection was setted up, go to the topic and from the "Properties" tab change the value from Byte Array to String and make click in the Update button.
  
  d) After we ran some enpoints, we'll be able to see the messages coming in the partition of the topic behind the column "Value".

 
![image](https://github.com/alejandronpereira/N5Challenge/assets/6674605/303b4a62-4105-4c1f-90b5-c25be878a674)
![image](https://github.com/alejandronpereira/N5Challenge/assets/6674605/4034ea27-3ead-4627-beaf-799c776cc17a)
![image](https://github.com/alejandronpereira/N5Challenge/assets/6674605/80c8f85d-0ce4-432a-bdf2-12b2ce80baa5)
![image](https://github.com/alejandronpereira/N5Challenge/assets/6674605/d53161f0-d024-4257-b260-49964629607e)
![image](https://github.com/alejandronpereira/N5Challenge/assets/6674605/17f1b096-b1f7-447e-b421-93a6a07c61a7)


