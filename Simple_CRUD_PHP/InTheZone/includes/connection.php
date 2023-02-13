<?php
//inlcude the constants syntax
require("constants.php");

//create a database
$connection=mysqli_connect(DB_SERVER,DB_USER,DB_PASSWORD);

//if the connection is null, then it has failed, show error message
if (!$connection){
    die("database connection failed:" . mysqli_error());
}

//select the database to use
$db_select=mysqli_select_db($connection, DB_NAME);
if(!$db_select){
    die("database selection failed:". mysqli_error());
}

?>