### Feedback

*Please add below any feedback you want to send to the team*


The token:
**Read-only Token** Header Name: `ApiKey` | Header Value : `MTIzNHxSZWFkg` it is not a valid token!
Here is the exception:
FormatException: The input is not a valid Base-64 string as it contains a non-base 64 character,
more than two padding characters, or an illegal character among the padding characters.

For this reason on ShowTimeController in Get method I commented the authorization policy: [Authorize(Policy = "Read")]
