<?php require_once("includes/functions.php"); ?>
<?php
    //a. Find Session
    session_start();

    //b. Unset all the session variables
    $_SESSION=array();

    //c. Destroy the session cookie
    if(isset($_COOKIE[session_name()])){
        setcookie(session_name(), "",time()-42000,'/');
    }

    //d. Destroy the session
    session_destroy();

    header(
        "location: login.php");
        exit;
    ?>