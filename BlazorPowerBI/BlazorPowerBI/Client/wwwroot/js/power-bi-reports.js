// https://docs.microsoft.com/en-us/javascript/api/overview/powerbi/configure-report-settings#hyperlink-click-behavior

/*const { Report } = require("./powerbi");*/

const models = window['powerbi-client'].models;

function embedPowerBIReport(biToken, embedReportId) {
    let embedConfiguration = {
        accessToken: biToken,
        embedUrl: 'https://app.powerbi.com/reportEmbed',
        id: embedReportId,
        permissions: models.Permissions.All,
        tokenType: models.TokenType.Embed,
        type: 'report',
        filters: [],
        settings: {
            panes: {
                filters: {
                    visible: false,
                },
                pageNavigation: {
                    visible: true,
                },
            },
        },
    };

    let embedContainer = document.getElementById('embedContainer');
    let report = powerbi.embed(embedContainer, embedConfiguration);

    report.on('dataHyperlinkClicked', (info) => {
        console.log(info.url);
    });
     report.on('dataSelected', (info) => {
        console.log(info.url);
    });
    report.off('loaded');
    report.on('loaded', function () {
        report.off('loaded');
    });
    report.on('error', function (event) {
        console.log(event.detail);
    });

    report.off('rendered');
    report.on('rendered', function () {
        report.off('rendered');
    });
}

async function getEmbeddedToken(bearerToken, payload) {
    let response = await fetch('https://api.powerbi.com/v1.0/myorg/GenerateToken',
        {
            method: 'POST',
            body: JSON.stringify(payload),
            headers: {
                'Content-type': 'application/json; charset=UTF-8',
                Authorization: 'Bearer ' + bearerToken,
            },
        }
    );
    if (response.ok) {
        let biResponse = await response.json();
        return biResponse;
    }
    else {
        console.log(await response.text());
        console.log(response.statusText);
    }
}

window.drawBi = async function (accessToken, embedReportId, payload) {
    var token = await getEmbeddedToken(accessToken, payload);
    await embedPowerBIReport(token.token, embedReportId);
}
