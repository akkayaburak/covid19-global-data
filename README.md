# COVID-19 Global Data

All data taken from [WHO](https://covid19.who.int/table).
One page website includes filtering dates, country and maximum cases.

## Technologies

- [.NET Core 2.2](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-3.1)
- [ElasticSearch](https://www.elastic.co/)
- [NEST](https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/nest.html)

## Usage
Download and setup ElasticSearch and Kibana.

Default access to ElasticSearch : `localhost:9200` 

You need to run `Kibana.bat` file in where you setup Kibana. 

Default access to Kibana : `localhost:5601` 

Download the CSV.

You can import the data from [Kibana](https://www.elastic.co/kibana) or [Logstash](https://www.elastic.co/logstash).



## License
[MIT](https://github.com/akkayaburak/covid19-global-data/blob/master/LICENSE)
