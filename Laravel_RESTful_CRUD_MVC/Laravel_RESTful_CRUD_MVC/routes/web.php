<?php

use App\Http\Controllers\MeetingController;
use App\Http\Controllers\RegistrationController;
use App\Http\Controllers\AuthController;

Route::group(['prefix' => 'api/v1'], function() {

    Route::resource('meeting', MeetingController::class)->except([
        'create', 'edit'
    ]);

    
    Route::resource('meeting/registration', RegistrationController::class)->only([
        'store', 'destroy'
    ]);

    
    Route::post('user', [AuthController::class, 'store'])->name('store');
    
    Route::post('user/signin', [AuthController::class, 'signin'])->name('signin');
});

/*Route::get('/', function () {
    return view('welcome');
 });*/
