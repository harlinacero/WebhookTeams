# Dillinger
## _The Last Markdown Editor, Ever_

[![N|Solid](https://cldup.com/dTxpPi9lDf.thumb.png)](https://nodesource.com/products/nsolid)

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

This project connect Api Teams webhook with some connections to some databases to build message dinamic.

- Create webhook in Teams app
- Add connection string of mongo or sql in localsettings.json
- Build message of body JSON of service and in this one, add the necesary for query in SQL or Mongo


## Example JSON

```sh
{
    "themeColor": "ff1100",
    "summary": "La entidad ha cambiado",
    "sections": [
        {
            "activityTitle": "La entidad x ha cambiado",
            "activitySubtitle": "En la colección: ",
            "activityImage": "",            
            "markdown": true
        }
    ],
    "urlTeams": "https://siigosa.webhook.office.com/webhookb2/3daba80e-a86c-469c-a78d-",
    "provider":"",
    "database":"Example",
    "collection":"JournalEntry",
    "filters": [
        {
            "field":"ModelType",
            "value":21,
            "compare":"="
        }
    ],
    "potentialAction": [{
        "@type": "ActionCard",
        "name": "Add a comment",
        "inputs": [{
            "@type": "TextInput",
            "id": "comment",
            "isMultiline": false,
            "title": "Add a comment here for this task"
        }],
        "actions": [{
            "@type": "HttpPOST",
            "name": "Add comment",
            "target": "https://docs.microsoft.com/outlook/actionable-messages"
        }]
    }, {
        "@type": "ActionCard",
        "name": "Set due date",
        "inputs": [{
            "@type": "DateInput",
            "id": "dueDate",
            "title": "Enter a due date for this task"
        }],
        "actions": [{
            "@type": "HttpPOST",
            "name": "Save",
            "target": "https://docs.microsoft.com/outlook/actionable-messages"
        }]
    }, {
        "@type": "OpenUri",
        "name": "Learn More",
        "targets": [{
            "os": "default",
            "uri": "https://docs.microsoft.com/outlook/actionable-messages"
        }]
    }, {
        "@type": "ActionCard",
        "name": "Change status",
        "inputs": [{
            "@type": "MultichoiceInput",
            "id": "list",
            "title": "Select a status",
            "isMultiSelect": "false",
            "choices": [{
                "display": "In Progress",
                "value": "1"
            }, {
                "display": "Active",
                "value": "2"
            }, {
                "display": "Closed",
                "value": "3"
            }]
        }],
        "actions": [{
            "@type": "HttpPOST",
            "name": "Save",
            "target": "https://docs.microsoft.com/outlook/actionable-messages"
        }]
    }]
}
```



