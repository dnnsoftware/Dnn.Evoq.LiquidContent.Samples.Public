from odoo import api, models, fields
from . import sc_api


class Configuration(models.Model):
    """ Class to store all the information needed for connecting to liquid content"""

    _name = 'connector_sc.configuration.backend'
    _description = 'Liquid Content Backend'
    _inherit = 'connector.backend'

    name = fields.Char()
    apiUrl = fields.Char(required=True, string="API URL")
    apiKey = fields.Char(required=True, string="API Key")
    apiVersion = fields.Char(required=False, string="Api version")
    apiKeyOk = fields.Char(required=False, string="Api Key Working")
    productTypeId = fields.Char(required=True, string="SC Product Type Id")

    version = fields.Selection(
        selection='_select_versions',
        string='Version',
        required=True,
    )

    @api.model
    def _select_versions(self):
        """ Available versions

        Can be inherited to add custom versions.
        """
        return [('1.14', 'Version 1.14')]

    @api.one
    def test_url_connection(self):
        version = sc_api.get_api_version(self.apiUrl)
        self.write({'apiVersion': version})

    @api.one
    def test_api_key_connection(self):
        result = sc_api.test_api_connection(self.apiUrl, self.apiKey)
        self.write({'apiKeyOk': result})

    @api.one
    def sync_all_products(self):
        products = self.env['product.product'].search([])
        for product in products:
            product.with_delay().export_product(product.id, self.id)
