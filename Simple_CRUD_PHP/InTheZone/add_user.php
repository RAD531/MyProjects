<!--include all the content in connectins page for connection to database, if failed, fatal error will show-->
<?php require_once("includes/connection.php"); ?>

<!--include all the content in functions page to run functions, if failed, fatal error will show-->
<?php require_once("includes/functions.php"); ?>

<?php
//run the checkSession function to check if the session exists
checkSession();
?>

<?php

//START FROM PROCESSING
//set up a new array to store the values to be submitted to database
//runs when the submit button is clicked
if (isset($_POST['submit'])) 
    { //Form has been submitted
        //grab the username input and store in username
    $username = trim($_POST['username']);
    //grab the password input and store in password
    $password = trim($_POST['password']);
    //hash the password
    $hashed_password = sha1($password);
    
    //if the username and password are not null
    if ($username != null && $password != null)
    {
    
    //query = insert username and password into users table with selected columns
    $query = "INSERT INTO users (
            username, Hashed_password
            ) VALUES (
                '{$username}', '{$hashed_password}'
            )";
        
        //grab the result for validation
        $result = mysqli_query($connection, $query);
        if ($result) 
            {
            $message = "The user was successfully created.";
            }
        else{
            $message = "The user could not be created.";
            $message .= "<br />" . mysqli_error($connection);
            }

    }

    //if the username or/and password is blank, show error message 
    else {
        print("username or password is blank");
    }
}
        else 
        { //Form has not been submitted
            $username = "";
            $password = "";
        }
        
?>

<?php include ("includes/header.php"); ?>

<!--if the message is not empty, print it on screen-->
<?php if (!empty($message)) {echo "<p class\='message\'>" . $message . "</p>";} ?>

<div id ="input">
<form action="add_user.php" method="post">
<br>
<table>
    <tr>
        <td>Username</td>
        <!--username input box = username var on submit-->
        <td><input type = "text" name="username" maxlength="30" value="<?php echo htmlentities($username); ?>" /></td>
    </tr>
    <tr>
        <!--password input box = password var on submit-->
        <td>Password</td>
        <td><input type="password" name="password" maxlength="30" value="<?php echo htmlentities($password); ?>" /></td>
    </tr>
    <tr>
        <td colspan="2"><input type="submit" name="submit" value="Create User" /></td>
    </tr>
</table>
</form>

<!--form to send the user back to the login page on submit-->
<form action="login.php" method = "post">
<input type ="submit" name="return" value="To Login Page" />
</form>
</div>

<!--include syntax from footer-->
<?php include ("includes/footer.php"); ?>
        