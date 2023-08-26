const connectionProgress = new signalR.HubConnectionBuilder()
    .withUrl("/fileUploadHub")
    .build();

connectionProgress.start().then(() => {
    console.log("Connected to SignalR hub.");
});

connectionProgress.on("ReceiveProgress", function (progress) {
    debugger
    console.log(`Upload Progress: ${progress}%`);
    // Update UI with progress information
    $("#progressBar").css("width", progress + "%");
    $("#progressBar").attr("aria-valuenow", progress);
});