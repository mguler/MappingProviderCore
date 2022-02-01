this library allows you configure object mappings in a seperate layer


### sample code
### sample code :

            //create a mapping service
            var mappingService = new MappingService();
@@ -20,3 +20,6 @@ this library allows you configure object mappings in a seperate layer
            var person = new Person { Name = "John Lee", Lastname = "Hooker", Birthdate = new DateTime(1927, 8, 22) };
            //then map it to an employee object
            var employee = mappingService.Map<Employee>(person);

            //Expected result is; John Lee Hooker,92,0 (age is 92 on 2019 and its value depends on the current year)  
            Console.Write($"{employee.Fullname},{employee.Age},{employee.Salary}");