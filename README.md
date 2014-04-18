CSharpRobloxAPI
===============

This allows you to go into contact with Roblox's API without doing the dirty work.

Documentation
===============

WARNING: Most of the methods do not use async! IT WILL INTTERUPT THE CURRENT THREAD! Use in a seperate thread.

This is not a complete documentation of the API. It's complete in the C# code. You may help with the documentation by creating a pull request.

#### RobloxApi.GetUserById
This method gets an user by id on the Roblox API.
Args: int uId - This is the id that is provided to get the user.
#### RobloxApi.GetUserBlurb
NOTE: There is another way to get the blurb without using this function, use an User object and the property Blurb.
Gets a user's blurb.
Args: User user - This is the user to get the blurb from.
#### RobloxApi.GetUserFriends
NOTE: There is another way to get the blurb without using this function, use an User object and the property Friends.
Gets user's friends.
Args: int userId - The user to get the friends from.
