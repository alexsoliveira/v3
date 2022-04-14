const host_name = "tgs.certificado";
let port = null;
let portTest = null;

//Comunicação entre aplicação web e extensão
chrome.runtime.onMessageExternal.addListener(
  function (request, sender, sendResponse) {

    switch (request.seq) {
      case "isInstalled":
        sendResponse(chrome.runtime.id);
        break;

      case "isHostInstalado":
        try {

          portTest = chrome.runtime.connectNative(host_name);

          portTest.onMessage.addListener(function (msgHost) {
            //Envia a msg para o front-end
            sendResponse(msgHost);
            portTest.disconnect();
          });
          portTest.onDisconnect.addListener(function (port) {
            let errorMsg;
            if (port && port.error && port.error.message) {
              errorMsg = port.error.message;
              sendResponse(false);
            } else if (chrome.runtime.lastError) {
              if (typeof chrome.runtime.lastError === 'string') {
                errorMsg = chrome.runtime.lastError;
                sendResponse(false);
              } else if (chrome.runtime.lastError.message) {
                errorMsg = chrome.runtime.lastError.message;
                sendResponse(false);
              }
            }
          });
          //Envia a msg para o backend
          portTest.postMessage(request);
        }
        catch (err) {
          portTest.disconnect();
          //portTest = null;
          //sendResponse(false);
        }

        break;

      case "lerCertificados":
        //Comunicação como o aplicativo host de leitura de certificados locais
        //Abre comunicação com o host backend
        port = chrome.runtime.connectNative(host_name);

        //Retorna a msg do backend
        port.onMessage.addListener((msgHost) => {
          //Envia a msg para o front-end
          sendResponse(msgHost);
          port.disconnect();

        });

        //Evento do serviço host desconectado
        port.onDisconnect.addListener(() => {
          //console.log("Serviço desconectado")
        });

        try {
          //Envia a msg para o backend
          port.postMessage(request);
        }
        catch (err) {
          sendResponse(false);
        }
        break;
    }
  }
);
