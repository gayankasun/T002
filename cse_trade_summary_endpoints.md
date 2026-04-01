# CSE Trade Summary – likely API endpoints for last close prices

Target UI page: `https://www.cse.lk/equity/trade-summary`

Because direct access to `cse.lk` returned HTTP 403 from this environment, these endpoints were inferred from a mirrored API documentation page and should be validated from your network.

## Primary endpoint (best quick approach)

- `GET https://www.cse.lk/api/tradeSummary`
  - Response root key: `reqTradeSummery`
  - Includes fields such as:
    - `symbol`
    - `price` (last traded price)
    - `previousClose` (last trading close price)
    - `change`
    - `percentageChange`

## Secondary endpoint (intraday snapshot)

- `GET https://www.cse.lk/api/todaySharePrice`
  - Includes:
    - `symbol`
    - `lastTradedPrice`
    - `change`
  - Does **not always** include `previousClose`, so it is less direct for close-price extraction.

## Optional detail endpoint

- `POST https://www.cse.lk/api/companyInfoSummery`
  - Form body: `symbol=<SYMBOL>`
  - Nested object `reqSymbolInfo` includes `previousClose` for a single symbol lookup.

## Minimal extraction pattern

1. Fetch `tradeSummary`.
2. Read `reqTradeSummery[*].symbol` and `reqTradeSummery[*].previousClose`.
3. Persist as your EOD close snapshot.

## Example jq extraction

```bash
curl -s https://www.cse.lk/api/tradeSummary \
| jq -r '.reqTradeSummery[] | [.symbol, .previousClose] | @csv'
```
