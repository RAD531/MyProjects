<!--run the session start pre-built function, assign user key to user machine-->
<?php session_start();?>
<!--include all the content in connectins page for connection to database, if failed, fatal error will show-->
<?php require_once("includes/connection.php"); ?>
<!--include all the content in functions page to run functions, if failed, fatal error will show-->
<?php require_once("includes/functions.php"); ?>


<?php

//START FORM PROCESSING
//if submit form is set, run the following code
//post = array of values from subit form
if (isset($_POST['submit'])) {
    $errors = array();
    //username = post array username value
    $username = trim($_POST['username']);
    //password = post array password value
    $password = trim($_POST['password']);
    //hash the password variable
    $hashed_password = sha1($password);

    //set up variable which equals matching username and password concatenated
    $query = "SELECT id, username ";
    $query .= "FROM users ";
    $query .= "WHERE username = '{$username}' ";
    //now thst username has been found, password must equal user id password, not any passsword in the table 
    $query .= "AND Hashed_password = '{$hashed_password}' ";
    //limit the result to just 1 row of data
    $query .= "LIMIT 1";
    //place the values of query into variable
    $result_set = mysqli_query($connection, $query);
    confirm_query($result_set);

    //if the number of rows = 1, then the username and password has a match in the database, proceed with login
    if (mysqli_num_rows($result_set) ==1){
        //username/password authenticated
        //and only 1 match

        //store result set array values in found user array from row of data from database
        $found_user = mysqli_fetch_array($result_set);

        //create session global variable user_id which equals user id from array
        $_SESSION['user_id']=$found_user['id'];
        //other variable in session array equals user name from array
        $_SESSION['user_name']=$found_user['username'];
        //send HTTP request to server to open staff page
        header("location: staff.php");
        //terminate script
        exit;
    }

    //if no rows were returned from the database, then username or/and password is wrong
    else{
        //username/password combo was not found in the database, display message
        $message = "Username/password combination incorrect.<br />
        Please make sure your caps lock key is off and try again.";
        
    }
}

    else { //Form has not been submitted, make sure form values are blank
        $username = "";
        $password = "";
    }
?>

<!--include the header page syntax, if unsucessfull, dont kil the page-->
<?php include ("includes/header.php"); ?>

<!--if message is not null, then display value of message as a paragrapth under the header-->
<?php if (!empty($message)) {echo "<p class\='message\'>" . $message . "</p>";} ?>


<div id = "input">
<!--create a form for submitting data-->
<form action="login.php" method="post">
<br>
<table>
    <tr>
        <td>Username:</td>
        <!--username = value in username textbox upon submit-->
        <td><input type = "text" name="username" maxlength="30" value="<?php echo htmlentities($username); ?>" /></td>
    </tr>

    <tr>
        <td>Password:</td>
        <!--password = value in password textbox upon submit-->
        <td><input type="password" name="password" maxlength="30" value="<?php echo htmlentities($password); ?>" /></td>
    </tr>
    <tr>
        <td colspan="2"><input type="submit" name="submit" value="Login" /></td>
    </tr>
</table>
</form>
<!--create another form to navigate the user back to the webiste-->
<form action="index.html" method = "post">
<input type ="submit" name="return" value="Back to Website" />
</form>
</div>

<!--include the footer page syntax, dont kill the page if failed-->
<?php include ("includes/footer.php"); ?>
        