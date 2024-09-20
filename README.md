Access the Swagger UI at [http://localhost:8080/swagger](http://localhost:8080/swagger) to interact with the API.

## API Endpoints

### Articles

- **GET /api/articles**: Get all articles (no authentication required).
- **POST /api/articles**: Create a new article (requires `EditArticle` policy).
- **PUT /api/articles/{id}**: Edit an article (requires `EditArticle` policy).
- **DELETE /api/articles/{id}**: Delete an article (requires `DeleteArticle` policy).

### Comments

- **GET /api/comments**: Get all comments (no authentication required).
- **POST /api/comments**: Create a new comment (requires `CommentOnArticle` policy).
- **PUT /api/comments/{id}**: Edit a comment (requires `EditComment` policy).
- **DELETE /api/comments/{id}**: Delete a comment (requires `DeleteComment` policy).
