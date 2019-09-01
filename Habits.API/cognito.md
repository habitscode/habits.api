# Create user in cognito
aws cognito-idp sign-up --region us-west-2 --client-id 2lk2djv25lhmptb5lv7i1634bc --username admin@example.com --password Passw0rd!

# Create initiate authentication
aws cognito-idp initiate-auth --auth-flow USER_PASSWORD_AUTH --client-id 2lk2djv25lhmptb5lv7i1634bc --auth-parameters USERNAME=admin@example.com,PASSWORD=Passw0rd!

# Use IdToken as Authorization header in API Gateway calls