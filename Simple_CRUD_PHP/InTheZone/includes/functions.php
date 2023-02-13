<?php 
//function to test database connection result
//if failed then dispplay error message
function confirm_query($result_set){
    if (!$result_set){
        die("database query failed" . mysqli_error($connection));
    }
}


//function to obtain page id
//set up variable page id
function get_page_by_id($page_id){
    //assign global variable
    global $connection;
    //mySQL query to obtain page data where page id var = pages page id
    $query="select * FROM pages WHERE id =" . $page_id . " LIMIT 1";
    //result_set = query and connection
    $result_set=mysqli_query($connection, $query);

    //if result set returns nothing then display connection error
    confirm_query($result_set);
    //REMEMBER
    // if no rows returned, fetch_array will return false
    if($page = mysqli_fetch_array($result_set)){
        //return the data for found page
        return $page;
}else{
    //return nothing if no rows found
    return NULL;
}
}

function find_selected_page(){

    //assign global variable
    global $sel_page;
    //if page exists, run the function 
    if(isset($_GET['page'])){
        //grab contents from page id from database
        //get page by id function does this
        $sel_page=get_page_by_id($_GET['page']);
    }else{
        //if no page, then return null
        $sel_page=NULL;
    }
}

function navigation(){
    //set up global var connection
    global $connection;
    //run mySQL query to grab all rows and data from pages
    $page_set= mysqli_query($connection, "SELECT * FROM pages");
    //if no data found, display SQL error message and kill the page
    if (!$page_set){
        die("database query failed: ".mysqli_error($connection));
    }
    //if data is found then print page name and link to page id
    echo "<ul class=\"pages\">";
    while($page=mysqli_fetch_array($page_set)){
        echo "<li><a href=\"content.php?page=".urlencode($page["id"])."\">".$page["Menu_name"]."</a></li>";
    }
    //echo the closing HTML tag
    echo "</ul>";
}

function checkSession(){

//run the session start pre-built function, assign user key to user machine
session_start();
    
//super global variable
//check if session exists
if(!isset($_SESSION['user_id'])){
    //if session does not exist under user id, then send user back to login page
    header("location: login.php");
    //terminate script
    exit;
}

}

?>
