
var wait = ms => new Promise((r, j) => setTimeout(r, ms))
var failed = false;

async function fetchAsync() {
    try {
        let res = await fetch(window.location.href) // ping local server
        //if failed before and now sucess then reload page
        if (failed && res.status === 200) {
            failed = false;
            location.reload();
        }
    }
    catch (e) {
        failed = true;
        await wait(200);
        console.debug("failed" + e)
    }
}

async function Pooling() {
    while (true) {
        fetchAsync();
        await wait(200);
    }
}

Pooling();
