<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->

- [Tech Stack](#tech-stack)
- [Microservices](#microservices)
- [JWT Endpoints](#jwt-endpoints)
  - [GET /internal/auth/access](#get-internalauthaccess)
  - [GET /auth/access](#get-authaccess)
- [User Endpoints](#user-endpoints)
  - [GET /login](#get-login)
  - [POST /register](#post-register)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

# Tech Stack
 - .NET
 - MSSQL
 - Docker

# Microservices
 - [X] User: User registration, login, and other account uses. Provides access tokens to other services. 
 - [X] Product: Getting and storing product information with company information. 
 - [ ] Shopping Cart: Handling user's cart and the products within them.

# JWT Endpoints

## GET /internal/auth/access


## GET /auth/access
```mermaid
sequenceDiagram
	participant MS as Microservice
	participant JWT as JWT Microservice
	
	MS ->>+ JWT: GET /auth/access, aud=_, internalAccessToken={}
	JWT ->>+ JWT: Validate access token
	alt Invalid Token
		JWT ->>+ MS: 401: unauthorized
		MS ->>+ JWT: GET /internal/auth/access, aud=jwt_service,info={}
		alt Valid information
			JWT ->>+ MS: 200: internalAccessToken={}
			Note right of MS: Try GET /auth/access one more time
			Note right of MS: Throw, error with jwt microservice authentication on fail
		else Invalid information
			JWT ->>+ MS: 401: unauthorized
			Note right of MS: Throw, error with secret
		end
	end
	JWT ->>+ MS: 200: signedToken={}
```

# User Endpoints

## GET /login
```mermaid
sequenceDiagram
	participant U as User
	participant LB as Load Balancer
	participant UM as User Microservice
	participant UDB as User Database
	U ->> LB: GET /login, info={}
	LB ->> UM: Route request
	UM ->> UDB: Check for user
	alt Valid user
		UM ->> U: 200: accessTokens=[{}]
	else User not found
		UM ->> U: 401: Unauthorized
	else Credentials not correct
		UM ->> U: 401: Unauthorized
	end
```

## POST /register
```mermaid
sequenceDiagram
	participant U as User
	participant LB as Load Balancer
	participant UM as User Microservice
	participant UDB as User Database
	participant JWT as JWT Microservice
	
	U ->> LB: POST /register, info={}
	LB ->> UM: Route request
	UM ->> UM: Check for existing user
	alt new user
		UM ->> UDB: Store new user
		UM ->> JWT: GET /auth/access, aud=user_service, internalAccessToken=""
		JWT <<->> UM: ...(See GET /auth/access)
		JWT ->> UM: 200: accessToken=""
		UM ->> JWT: GET /auth/access, aud=cart_service, internalAccessToken=""
		JWT <<->> UM: ...
		JWT ->> UM: 200: accessToken=""
		UM ->> U: 200: accessTokens=[{}]
	else existing user
		UM ->> U: 409: Duplicate user with username
	end
```



