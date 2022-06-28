# Contacts App

This application is created as an assignment by Taha Resuloğulları.
With this application, users can hold their Contact Info and easily get reports about locations.

## Installation

First, download and install [Docker Desktop](https://www.docker.com/products/docker-desktop/).
Then open the Docker application.
Lastly, open a terminal and write down the codes below.

```bash
# builds images
# it can take some time depending on your internet connection.
docker composer build

# builds and runs containers
docker-composer up
```
And it's all set.

## Usage
After all the services running, you can simply make requests for endpoints.

```REST
## Person
This endpoint is in
localhost:4042//


#{GET} /persons
returns person list

#{GET} /persons/{id}
returns person detail

#{POST} /persons
creates person

#{PUT} /persons/{id}
updates person

#{DELETE} /persons/{id}
deletes person
```

```REST
## Contact Information
This endpoint is in
localhost:4042//


#{GET} /contactinformations
returns contact information list

#{POST} /contactinformations/{personId}
creates contact information to specified person

#{PUT} /contactinformations/{id}
updates contact information

#{DELETE} /contactinformations/{id}
deletes contact information
```


```REST
## Reports
This endpoint is in
localhost:4043//


#{GET} /reports
returns reports list

#{GET} /reports/{id}
returns report details

#{GET} /reports/create
creates report request
After the report request is received, the request is adding Kafka Message Queue.
Report statuses can be checked with /reports endpoint

#/report/{fileUrl}
downloads the report
After the report is ready, they can be downloaded with this endpoint.
```
## TechStack
* [.Net6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
* [Confluent Kafka](https://docs.confluent.io/platform/current/installation/installing_cp/zip-tar.html)
* [MongoDB](https://github.com/mongodb/mongo-csharp-driver)
* [PostgreSQL](https://github.com/postgres/postgres)

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)
