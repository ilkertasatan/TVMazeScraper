
#!/bin/bash
echo "Building applications."

docker-compose down

docker-compose build

docker-compose up -d

echo "Built and running applications."
