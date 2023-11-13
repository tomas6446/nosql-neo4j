# nosql-neo4j
Neo4jdb dotnet example

```bash
  # Clone repository
  git clone https://github.com/tomas6446/nosql-neo4j

  # Enter the directory
  cd nosql-neo4j

  # Create docker container
  sudo docker compose up -d
  
  # Copy and run the script into container
  sudo docker cp init.cypher nosql-neo4j:/init.cypher
  sudo docker exec nosql-neo4j cypher-shell -u neo4j -p password -f /init.cypher

  # Build the project
  dotnet build

  # Run the project
  dotnet run 
```

## Access the container's cypher-shell
```bash
  sudo docker exec -it nosql-neo4j cypher-shell -u neo4j -p password
```
