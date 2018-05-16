# How to build/run Secure user data sample

* Set password with the Secret Manager tool:

  `dotnet user-secrets set SeedUserPW <pw>`
  `dotnet user-secrets set SMTPUserPW <pw>`
* Update the database:

	`dotnet ef database update`

* Enable SSL in the project