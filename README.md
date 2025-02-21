[Confluence page with all detailed information about the framework and contract testing TBD.](https://mews.atlassian.net/)

## Set Up
1. Clone the project from [Contract.Test.Template](https://github.com/MewsSystems/Contract.Tests.Template)
2. Add .env files in both the Consumer.Tests and Provider.Tests projects, under the root folder (you can use example.env files as template)
3. Set Up your own data
4. Run the project for any project from the IDE or using 'dotnet test' command in the project folder

## Contribution Rules

#### Branch names:
- All branches should be created from Jira tasks and start with Jira task ID, in format like this MMP-1234-Some-feature

#### Commit messages:
- Should be descriptive and meaningful, avoid messages like “fix”, “new test” or so. Ideally follow same convention as branch names MMP-1234: Adding logic to parse data

#### Pull request:
- Title should start with Jira task ID, in format like this: MMP-1234-Some-feature
- No work should be done on active PR, instead of comment fixes. PR should be changed to draft or comment submitted before change implementation.

#### Comments:
- Comments should be added in batches, once the review is done, not as a single comments.
- Author should state implementation status in comment response.
- Only commenter or some other independent reviewer, with the same level of seniority, should resolve a comment.

#### Review results:
- Approve - Negligible or no issues found. Doesn’t require re-approval after fixes were implemented.
- Comment - Some further work needs to be done and checked once finished.
- Request changes - Critical issues found, like exposed credentials, breaking change, destruction of test env, .... (only requester/admin can approve/merge, so think twice if you go for vacation)

#### Merge:
- If all checks pass and PR is approved, anyone can merge it.
- All PRs should be squashed and merged (ideally enforced in repository settings).
