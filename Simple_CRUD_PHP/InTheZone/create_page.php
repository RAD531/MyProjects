<!--include all the content in connectins page for connection to database, if failed, fatal error will show-->
<?php require_once("includes/connection.php"); ?>
<!--include all the content in functions page to run functions, if failed, fatal error will show-->
<?php require_once("includes/functions.php"); ?>

<?php
//run the checkSession function to check if the session exists
checkSession();
?>

<?php
    //to be used to post the menu name to the pages table in database
    $menu_name=$_POST['menu_name'];
    //to be used to post the content to the pages table in database
    $content=$_POST['content'];

    //if the menu_name inut box is blank then give it a default name
    if ($menu_name==null){
        $menu_name="blank name given";
    }
?>

<?php
    //in SQL command string values has to be between single quote ''
    //insert menu name and content into the pages table
    $query="INSERT INTO pages(menu_name,content)";
    $query.=" VALUES ('{$menu_name}','{$content}')";
    //test if the insert was true
    if(mysqli_query($connection, $query)){
        //Success!
        header("location: content.php");
        exit;
    //if false, then display error message
    }else{
        echo "<p> Subject creation failed.</p>";
        echo "<p>".mysqli_error($connection)."</p>";
    }
?>

<?php
//close the connection manually becuase footer not included
if(isset($connection)){
    mysql_close($connection);
}
?>
        