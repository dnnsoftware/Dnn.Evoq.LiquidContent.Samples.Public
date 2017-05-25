# -*- coding: utf-8 -*-
###############################################################################
# Connector between Odoo and Evoq Liquid Content
###############################################################################
{
    "name": "Connector with Evoq Liquid Content",
    "version": "1.0.1",
    "depends": [
        "product",
        "web",
        "connector"        
    ],
    "author": "DnnSoftware",
    "description": """ A connector between Oddo and Evoq Liquid Content """,
    'images': [
    ],
    "website": "http://www.dnnsoftware.com",
    "category": "Connector",
    "demo": [],
    "data": [
        'views/configuration.xml',
        'views/liquid_content_menu.xml'
    ],
    'css':  [],
    'js':   [],
    'qweb': [],
    "active": False,
    "installable": True,
    "application": True
}
